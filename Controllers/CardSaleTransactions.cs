using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SQL.Controllers
{

    [Route("CardsaleTransActions/[controller]")]
    [ApiController]
    public class CardSaleTransactions : ControllerBase
    {

        // GET: CardsaleTransActions/LatestCompanyTraceNo
        [HttpGet("/{CompanyNo}/{Password}/LatestCompanyTraceNo")]
        public int Get1(int CompanyNo,string Password)
        {
            int result = -1;
            Console.WriteLine("yes1");
            DBConnect db = new DBConnect();
            int userID = db.SQLGetUser(CompanyNo, Password);
            if(userID != -1 )
            {
                Console.WriteLine("the user was found "+userID);
                result = userID;
            }
            else
            {
                Console.WriteLine("A user will be created");
                int newUserID = db.SQLNewUser(CompanyNo, Password);
                Console.WriteLine("New user with ID "+newUserID);
                result = newUserID;
            }
            return db.SQLLatest(result);

        }

        // GET CardsaleTransActions/
        [HttpGet("/{CompanyNo}/{Password}")]

        public List<Models.CardSaleTransactions>? Get2(int CompanyNo, string Password) //ActionResult<CardSaleTransactions>
        {
            DBConnect db = new DBConnect();
            int userID = db.SQLGetUser(CompanyNo, Password);
            string table = db.GetTheUsersTable(userID);

            List<Models.CardSaleTransactions> cardSaleTransactions = db.SQLGetCardSaleTransactions(table);
            return cardSaleTransactions;
        }

        // POST CardsaleTransActions/
        [HttpPost("/{CompanyNo}/{Password}")]
        public void Post(int CompanyNo, string Password,[FromBody] Models.CardSaleTransactions value)
        {
        
            DBConnect db = new DBConnect();
            int userID = db.SQLGetUser(CompanyNo, Password);
            string table = db.GetTheUsersTable(userID);

            db.INSERTCardSaleTransactions( table, value);
        }

    }


}
