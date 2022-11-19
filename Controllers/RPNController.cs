using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace KATA_RPN.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class RPNController : Controller
    {
        // GET: RPNController/GetStack
        [HttpGet]
        public ActionResult GetStack()
        {
            return Ok(JsonConvert.SerializeObject(Stack.stack));
        }

        // DELETE: RPNController/FlushStack
        [HttpDelete]
        public ActionResult Delete()
        {
            return Ok(JsonConvert.SerializeObject(Stack.FlushStack()));
        }

        /// <summary>
        /// User input.
        /// </summary>
        /// <param name="input">An int to ad to the stack or an operator (+ - / *)</param>
        /// <returns>Either the stack state, the result of an operation or an error if the stack doesn't have enough ints</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST RPNController/InputValue
        ///     {
        ///        "input": "10"
        ///     }
        ///
        /// </remarks>
        // POST: RPNController/InputValue
        [HttpPost]
        public ActionResult InputValue(Input input)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(input.Value))
                {
                    string inputValue = input.Value;
                    inputValue = inputValue.Trim();
                    if (Int32.TryParse(inputValue, out _))
                    {
                        return Ok(JsonConvert.SerializeObject(Stack.AddValue(Int32.Parse(inputValue))));
                    }
                    else if ("+-/*".Contains(inputValue))
                    {
                        (int, int) lastValues = Stack.TakeLastTwoValues();
                        string result = String.Empty;
                        switch (inputValue)
                        {
                            case "+":
                                result = (lastValues.Item1 + lastValues.Item2).ToString();
                                break;
                            case "-":
                                result = (lastValues.Item1 - lastValues.Item2).ToString();
                                break;
                            case "/":
                                result = Math.Round((decimal)lastValues.Item1 / (decimal)lastValues.Item2, MidpointRounding.ToEven).ToString(); // Résultera en des arrondis puisque l'on ne va insérer que des int dans la stack
                                break;
                            case "*":
                                result = (lastValues.Item1 * lastValues.Item2).ToString();
                                break;
                        }
                        return Ok(JsonConvert.SerializeObject(Stack.AddValue(Int32.Parse(result))));
                    }
                    else return UnprocessableEntity();
                }
                else return BadRequest();
            }
            catch (Exception e)
            {
                return Problem(detail: e.StackTrace + " : " + e.Message);
            }
        }
    }
}
