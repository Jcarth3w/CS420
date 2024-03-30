using Microsoft.AspNetCore.Mvc; 

namespace WebApi.Controllers; 

[ApiController] 

[Route("[controller]/[action]")] 

public class FruitsController : ControllerBase 
{ 
    public static List<string> AllFruits = new List<string> { "Apple", "Pear", "Orange" }; 
    [HttpGet] 
    public List<string> GetAll() 
    { 

        return new List<string> { "Apple", "Pear", "Orange" }; 
    } 

    [HttpPost]
    public void Add([FromBody]string fruit)
    {
        AllFruits.Add(fruit);
    }

} 

