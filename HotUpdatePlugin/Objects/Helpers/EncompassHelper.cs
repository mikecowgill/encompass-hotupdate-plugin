﻿using Lendmatic.HotUpdatePlugin.Objects.Extension;
using Lendmatic.HotUpdatePlugin.Objects.Models;
using EllieMae.EMLite.ClientServer;
using EllieMae.EMLite.Common;
using EllieMae.EMLite.ContactUI;
using EllieMae.EMLite.DataEngine;
using EllieMae.EMLite.RemotingServices;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.BusinessObjects.Users;
using EllieMae.Encompass.Collections;
using EllieMae.Encompass.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Windows.Forms;
using Persona = EllieMae.Encompass.BusinessObjects.Users.Persona;

namespace Lendmatic.HotUpdatePlugin.Objects.Helpers
{
    public static class EncompassHelper
    {
        public static bool IsTest()
        {
            return EncompassApplication.Session.ServerURI.Contains(CDOHelper.CDO.PluginSettings.TestServer);
        }

        public static string FieldDescription(string FieldID)
        {
            return EncompassApplication.Session.Loans.FieldDescriptors[FieldID].Description;
        }

        public static string LastPersona
        {
            get
            {
                return EncompassHelper.User.Personas?.Cast<Persona>().LastOrDefault().Name ?? string.Empty;
            }
        }
        public static string[] GetReportValues(string[] fields, string guid)
        {
            string[] result = new string[fields.Length];
            StringList fieldsToAdd = new StringList();
            foreach (string field in fields)
                fieldsToAdd.Add($"Fields.{field}");
            LoanReportData data = EncompassApplication.Session.Reports.SelectReportingFieldsForLoan(guid, fieldsToAdd);
            for (int i = 0; i < fields.Length; i++)
                result[i] = data[$"Fields.{fields[i]}"].ToString();

            return result;
        }
        public static void SendEmail(MailMessage Message)
        {
            try
            {
                ContactUtils.SendMail(Message);
            }
            catch (Exception ex)
            {
                Logger.HandleError(ex, nameof(SendEmail));
            }
        }

        public static Loan Loan
        {
            get { return EncompassApplication.CurrentLoan; }
        }

        public static LoanDataMgr LoanDataManager {get{ return RemoteSession.LoanDataMgr; }}

        public static string LoanNumber()
        {
            return Loan.LoanNumber;
        }

        public static User User
        {
           get { return EncompassApplication.CurrentUser; }
        }

        public static bool ContainsPersona(List<string> p)
        {
            return p.Any(x => EncompassApplication.CurrentUser.Personas.Contains(PersonaByName(x)));
        }

        public static string Formatted(string FieldID, string Value)
        {
            return EncompassApplication.CurrentLoan.Fields[FieldID].FormattedValue;
        }

        public static bool IsSuper { get { return EncompassApplication.CurrentUser.Personas.Contains(EncompassApplication.Session.Users.Personas.SuperAdministrator); } }
        public static List<FieldDescriptor> GetPrefixedFields(string Prefix)
        {
            return EncompassApplication.Session.Loans.FieldDescriptors.Cast<FieldDescriptor>().Where(x => x.FieldID.StartsWith(Prefix)).ToList<FieldDescriptor>();
        }

        private static Persona PersonaByName(string name)
        {
            return EncompassApplication.Session.Users.Personas.GetPersonaByName(name);
        }

        public static EllieMae.EMLite.RemotingServices.Sessions.Session RemoteSession
        {
            get { return EllieMae.EMLite.RemotingServices.Session.DefaultInstance; }
        }

        public static SessionObjects SessionObjects
        {
            get { return RemoteSession.SessionObjects; }
        }
        public static void ShowOnTop(string Title, string Message)
        {
            MessageBox.Show(Message, Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        public static string Val(string FieldID)
        {
            if (EncompassApplication.CurrentLoan == null)
                return string.Empty;

            if (!EncompassApplication.Session.Loans.FieldDescriptors[FieldID].MultiInstance)
                return EncompassApplication.CurrentLoan.Fields[FieldID].Value?.ToString() ?? string.Empty;
            else
                return string.Empty;
        }
        public static List<EncompassLog> ReadLog(string Search, int SeekPos = 0, bool Performance = false)
        {
            List<EncompassLog> result = new List<EncompassLog>();

            using (FileStream f = File.Open(Performance ? $"{PerformanceMeter.FilePath}/perf.log" : Tracing.LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                f.Seek(SeekPos, SeekOrigin.Begin);
                using (StreamReader sr = new StreamReader(f))
                {
                    while(!sr.EndOfStream)
                    {
                        string message = string.Empty;
                        string stamp = string.Empty;
                        string error = string.Empty;
                        if(!Performance)
                        {
                            string line = sr.ReadLine();
                            string[] s = line.Split(']');
                            if(s.Count().Equals(1))
                            {
                                s = line.Split(':');
                                error = s[0];
                                if (s.Count().Equals(2))
                                    message = s[1];
                            }
                            else if(!line.StartsWith("["))
                            {
                                message = line;
                            }
                            else
                            {
                                stamp = s[0].Replace("[", string.Empty);
                                string[] s2 = s[1].Split(':');
                                error = s2[0];
                                message = $"{s2[1]} : { (s2.Count().Equals(3) ? s2[2] : "")}";
                            }
                        }
                        else
                        {
                            message = sr.ReadLine();
                            error = "Performance";
                        }

                        if ((!Search.Empty() && !message.Contains(Search)) || (Performance && !PerformanceMeter.Enabled))
                            continue;

                        result.Add(new EncompassLog()
                        {
                            TimeStamp = stamp.Empty() ? (DateTime?)null : DateTime.Parse(stamp),
                            Message = message,
                            Type = error.Contains("VERBOSE") ? Enums.EncompassLogType.Verbose
                                                             : error.Contains("INFO") ? Enums.EncompassLogType.Info
                                                             : error.Contains("Application") ? Enums.EncompassLogType.Application
                                                             : error.Contains("START") ? Enums.EncompassLogType.TimerStart
                                                             : error.Contains("STOP") ? Enums.EncompassLogType.TimerStop
                                                             : Enums.EncompassLogType.Error
                        });
                    }
                }
            }

            return result;
        }

        public static void Set(string FieldID, string Value, string Index = null)
        {
            if (string.IsNullOrEmpty(Index))
                EncompassApplication.CurrentLoan.Fields[FieldID].Value = Value;
            else
                EncompassApplication.CurrentLoan.Fields[Loan.Fields.GetFieldAt(FieldID, Convert.ToInt32(Index)).ID].Value = Value;
        }

        public static void SetBlank(string FieldID, string Index = null)
        {
            if (string.IsNullOrEmpty(Index))
                EncompassApplication.CurrentLoan.Fields[FieldID].Value = string.Empty;
            else
                EncompassApplication.CurrentLoan.Fields[Loan.Fields.GetFieldAt(FieldID, Convert.ToInt32(Index)).ID].Value = string.Empty;
        }


    }
}
