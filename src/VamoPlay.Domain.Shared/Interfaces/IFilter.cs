namespace VamoPlay.Domain.Shared.Interfaces
{
    public interface IFilter
    {
        void Validate();
        int GetPageNumber();
        int GetPageSize();
    }
}
