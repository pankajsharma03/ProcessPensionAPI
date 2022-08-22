using ProcessPensionAPI.Model;

namespace ProcessPensionAPI.Provider
{
    public interface IRequestProvider
    {
        public PensionDetail ProcessPension(ProcessPensionInput input, string token);

    }
}
