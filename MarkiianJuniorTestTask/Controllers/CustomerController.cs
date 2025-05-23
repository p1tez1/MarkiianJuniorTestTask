using Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly IMediator _mediator;

        public CustomerController(ILogger<CustomerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("only-tv", Name = "GetOnlyTvCustomers")]
        public async Task<IActionResult> GetOnlyTvCustomers()
        {
            _logger.LogInformation("Request received: GetOnlyTvCustomers");

            try
            {
                var customers = await _mediator.Send(new GetOnlyTvCustomersQuery());

                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("No TV-only customers found.");
                    return NotFound("No TV-only customers found.");
                }

                _logger.LogInformation("TV-only customers found: {Count}", customers.Count());
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching TV-only customers.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("only-dsl", Name = "GetOnlyDslCustomers")]
        public async Task<IActionResult> GetOnlyDslCustomers()
        {
            _logger.LogInformation("Request received: GetOnlyDslCustomers");

            try
            {
                var customers = await _mediator.Send(new GetOnlyDslCustomersQuery());

                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("No DSL-only customers found.");
                    return NotFound("No DSL-only customers found.");
                }

                _logger.LogInformation("DSL-only customers found: {Count}", customers.Count());
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching DSL-only customers.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("only-overlapping-tv", Name = "GetOverlappingTvProducts")]
        public async Task<IActionResult> GetOnlyOverlappingTvProducts()
        {
            _logger.LogInformation("Request received: GetOnlyOverlappingTvProducts");

            try
            {
                var customers = await _mediator.Send(new GetOnlyOverlappingTvProductsQuery());

                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("No overlapping TV products found.");
                    return NotFound("No overlapping TV products found.");
                }

                _logger.LogInformation("Overlapping TV products found: {Count}", customers.Count());
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching overlapping TV products.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("only-matched-customer", Name = "GetMatchedCustomer")]
        public async Task<IActionResult> GetMatchedCustomer()
        {
            _logger.LogInformation("Request received: GetMatchedCustomer");

            try
            {
                var matchedCustomer = await _mediator.Send(new GetOnlyMatchedCustomerQuery());

                if (matchedCustomer == null || !matchedCustomer.Any())
                {
                    _logger.LogWarning("No matched customers found.");
                    return NotFound("No matched customers found.");
                }

                _logger.LogInformation("Matched customers retrieved: {Count}", matchedCustomer.Count());
                return Ok(matchedCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving matched customers.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
        [HttpGet("only-matched-customer-othewise", Name = "GetMatchedCustomerAndPostIntoMathcedCustomer")]
        public async Task<IActionResult> GetMatchedCustomerOtherwise()
        {
            _logger.LogInformation("Request received: GetMatchedCustomer");

            try
            {
                var matchedCustomer = await _mediator.Send(new GetOnlyMatchedCustomerQueryOtherwise());

                if (matchedCustomer == null || !matchedCustomer.Any())
                {
                    _logger.LogWarning("No matched customers found.");
                    return NotFound("No matched customers found.");
                }

                _logger.LogInformation("Matched customers retrieved: {Count}", matchedCustomer.Count());
                return Ok(matchedCustomer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving matched customers.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}

