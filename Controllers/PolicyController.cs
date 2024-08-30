[ApiController]
//[Route("api/[controller]")]
public class PoliciesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PoliciesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetPolicies([FromQuery] string searchCriteria)
    {
        var policies = _context.Policies.Include(p => p.Customer).AsQueryable();

        if (!string.IsNullOrEmpty(searchCriteria))
        {
            policies = policies.Where(p => p.PolicyNumber.Contains(searchCriteria) ||
                                           p.Customer.Name.Contains(searchCriteria));
        }

        return Ok(policies.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult GetPolicy(int id)
    {
        var policy = _context.Policies.Include(p => p.Customer).SingleOrDefault(p => p.PolicyId == id);
        if (policy == null)
        {
            return NotFound();
        }

        return Ok(policy);
    }

    [HttpPut("{id}")]
    public IActionResult UpdatePolicy(int id, [FromBody] Policy policy)
    {
        if (id != policy.PolicyId)
        {
            return BadRequest();
        }

        var existingPolicy = _context.Policies.Find(id);
        if (existingPolicy == null)
        {
            return NotFound();
        }

        existingPolicy.Phone = policy.Phone;
        existingPolicy.Email = policy.Email;
        existingPolicy.Address = policy.Address;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public IActionResult AddPolicy([FromBody] Policy policy)
    {
        _context.Policies.Add(policy);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetPolicy), new { id = policy.PolicyId }, policy);
    }
}
