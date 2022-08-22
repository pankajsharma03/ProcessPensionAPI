using ProcessPensionAPI.Model;

namespace ProcessPensionAPI.Repository
{
    public interface IRequestRepository
    {
        public PensionDetail ProcessPension(ProcessPensionInput input, string token);

    }
}
