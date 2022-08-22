using ProcessPensionAPI.Model;
using ProcessPensionAPI.Repository;

namespace ProcessPensionAPI.Provider
{
    public class RequestProvider : IRequestProvider
    {


        IRequestRepository _repo;

        public RequestProvider(IRequestRepository repo)
        {
            _repo = repo;
        }

        public PensionDetail ProcessPension(ProcessPensionInput input, string token)
        {
            return _repo.ProcessPension(input, token);
        }
    }
}
