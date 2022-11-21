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
        public ActionResult FlushStack()
        {
            return Ok(JsonConvert.SerializeObject(Stack.FlushStack()));
        }

        /// <summary>
        /// User numbers input.
        /// </summary>
        /// <param name="input">An int to ad to the stack</param>
        /// <returns>Either the stack state or an error if the stack doesn't have enough ints</returns>
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
        [HttpPost("InputValue")]
        public ActionResult InputValue(string input)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(input))
                {
                    input = input.Trim();
                    if (Int32.TryParse(input, out _))
                    {
                        return Ok(JsonConvert.SerializeObject(Stack.AddValue(Int32.Parse(input)).ToList()));
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

        /// <summary>
        /// User operators input.
        /// </summary>
        /// <param name="input">An operator to use on the last 2 values in the stack (+ - / *)</param>
        /// <returns>Either the stack state, with the result of an operation as the last element, or an error if the stack doesn't have enough ints</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST RPNController/Operate
        ///     {
        ///        "input": "+"
        ///     }
        ///
        /// </remarks>
        // POST: RPNController/Operate
        [HttpPost("Operate")]
        public ActionResult Operate(string input)
        {
            try
            {
                if(Stack.StackCount() < 2)
                {
                    return Problem(detail: "Nombre d'éléments dans la stack inférieur à 2, impossible de calculer");
                }
                if (!String.IsNullOrWhiteSpace(input))
                {
                    input = input.Trim();
                    if (!input.Any(x => !"+-/*".Contains(x)) && input.Length <= Stack.StackCount() - 1)
                    {
                        foreach (char currentInput in input)
                        {
                            (int, int) lastValues = Stack.TakeLastTwoValues();
                            int? result = null;
                            switch (currentInput)
                            {
                                case '+':
                                    result = lastValues.Item1 + lastValues.Item2;
                                    break;
                                case '-':
                                    result = lastValues.Item1 - lastValues.Item2;
                                    break;
                                case '/':
                                    result = (int?)Math.Round((decimal)lastValues.Item1 / (decimal)lastValues.Item2, MidpointRounding.ToEven); // Résultera en des arrondis puisque l'on ne va insérer que des int dans la stack
                                    break;
                                case '*':
                                    result = lastValues.Item1 * lastValues.Item2;
                                    break;
                            }
                            if (result == null)
                            {
                                return Problem(detail: "Aucun calcul de fait");
                            }
                            Stack.AddValue(result.Value);
                        }
                        return Ok(JsonConvert.SerializeObject(Stack.GetStack().ToList()));
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
