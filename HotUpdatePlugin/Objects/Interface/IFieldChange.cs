using EllieMae.Encompass.BusinessObjects.Loans;

namespace Lendmatic.HotUpdatePlugin.Objects.Interface
{
    public interface IFieldChange
    {
        void FieldChanged(object sender, FieldChangeEventArgs e);
    }
}
