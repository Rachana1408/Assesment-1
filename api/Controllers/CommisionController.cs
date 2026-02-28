using Microsoft.AspNetCore.Mvc;

namespace AvalphaTechnologies.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommisionController : ControllerBase
    {
        private const decimal AvalphaLocalRate = 0.20m;
        private const decimal AvalphaForeignRate = 0.35m;
        private const decimal CompetitorLocalRate = 0.02m;
        private const decimal CompetitorForeignRate = 0.0755m;

        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        [ProducesResponseType(400)]
        [HttpPost]
        public IActionResult Calculate([FromBody] CommissionCalculationRequest calculationRequest)
        {
            if (!IsValid(calculationRequest, out var error)) //Checking...whether user input is valid or not
                return BadRequest(error);
            
            //Actual Calculation of AvalphaTechnologiesCommissionAmount and CompetitorCommissionAmount
            var avalphaLocal = AvalphaLocalRate *
                               calculationRequest.LocalSalesCount *
                               calculationRequest.AverageSaleAmount;

            var avalphaForeign = AvalphaForeignRate *
                                 calculationRequest.ForeignSalesCount *
                                 calculationRequest.AverageSaleAmount;

            var competitorLocal = CompetitorLocalRate *
                                  calculationRequest.LocalSalesCount *
                                  calculationRequest.AverageSaleAmount;

            var competitorForeign = CompetitorForeignRate *
                                    calculationRequest.ForeignSalesCount *
                                    calculationRequest.AverageSaleAmount;

            var response = new CommissionCalculationResponse
            {
                AvalphaTechnologiesCommissionAmount = avalphaLocal + avalphaForeign,
                CompetitorCommissionAmount = competitorLocal + competitorForeign
            };

            return Ok(response); //return the response to frontend 
        }

        private bool IsValid(CommissionCalculationRequest request, out string error)
        {
            error = string.Empty;

            if (request == null)
            {
                error = "Request cannot be null.";
                return false;
            }

            if (request.LocalSalesCount < 0 || request.ForeignSalesCount < 0)
            {
                error = "Sales count cannot be negative.";
                return false;
            }

            if (request.AverageSaleAmount <= 0)
            {
                error = "Average sale amount must be greater than zero.";
                return false;
            }

            if (request.LocalSalesCount > 100000 || request.ForeignSalesCount > 100000)
            {
                error = "Sales count exceeds expected limit.";
                return false;
            }

            return true;
        }
    }

    public class CommissionCalculationRequest
    {
        public int LocalSalesCount { get; set; }
        public int ForeignSalesCount { get; set; }
        public decimal AverageSaleAmount { get; set; }
    }

    public class CommissionCalculationResponse
    {
        public decimal AvalphaTechnologiesCommissionAmount { get; set; }

        public decimal CompetitorCommissionAmount { get; set; }
    }
}
