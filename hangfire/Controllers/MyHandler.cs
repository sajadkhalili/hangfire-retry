using hangfire.Controllers;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Hangfire.Storage;

namespace hangfire.Controllers
{
    public class MyHandler
    {
        private readonly ILogger<MyHandler> _logger;
        private readonly IBackgroundJobClient _backgroundJobClient;

        public MyHandler(ILogger<MyHandler> logger, IBackgroundJobClient backgroundJobClient)
        {
            _logger = logger;
            _backgroundJobClient = backgroundJobClient;
        }

        public Task RunProcess()
        {
            _logger.LogInformation("method RunProcess  before call command1");
            _backgroundJobClient.Enqueue<MyHandler>(exp => exp.Command1());
            _logger.LogInformation("method RunProcess  after call command1");
            return Task.CompletedTask;
        }
    
     //   [GholiExceptionNoRetry]
        public Task Command1()
        {

            throw new NotSupportedException();
            _logger.LogInformation("method command1  before call command2");
            _backgroundJobClient.Enqueue<MyHandler>(exp => exp.Command2());

            _logger.LogInformation("method command1  after call command2");
            return Task.CompletedTask;
        }


        public Task Command2()
        {
         

            _logger.LogInformation("method command2 ");
            return Task.CompletedTask;
        }

    }


        public sealed class GholiExceptionNoRetryAttribute : JobFilterAttribute, IElectStateFilter
        {
    
            public void OnStateElection(ElectStateContext context)
            {
                var failedState = context.CandidateState as FailedState;
                if (failedState == null)
                {
                    // This filter accepts only failed job state.
                    return;
                }

                if (failedState.Exception != null && failedState.Exception is not NotSupportedException)
                {
                    context.CandidateState = new DeletedState(new ExceptionInfo(failedState.Exception))
                    {
                        Reason = "Error code"
                    };
                }
        }

        }

}
