using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MegaCasinoChallenge
{
    public partial class index : System.Web.UI.Page
    {
        Random random = new Random();
        string[] imgArray = new string[] {"Bar.png", "Bell.png", "Cherry.png", "Clover.png", "Diamond.png",
            "HorseShoe.png", "Lemon.png", "Orange.png", "Plum.png", "Seven.png", "Strawberry.png", "Watermelon.png"};
        double playerMoney;

        protected void Page_Load(object sender, EventArgs e)
        {
            //initial settings - three random pictures and $100
            if (!Page.IsPostBack)
            {
                playerMoney = 100.00;
                selectImages();
                lblPlayerMoney.Text = String.Format("{0:C}", playerMoney);
            }
        }


        //on button click
        protected void btnPullLever_Click(object sender, EventArgs e)
        {
            if (isNotANumber() == true || tooManyDecimals() == true || isBetNegative() == true || isBetTooHigh() == true)
            {
                stopGame();
            }
            else
            {
                selectImages();
                playerMoney = convertPlayerMoneyToDouble();
                playGame();
            }
        }

        //play the game if win then loser() else winner()
        private void playGame()
        {
            loser();
            winner();
        }

        //perform if there is invalid information
        private void stopGame()
        {
            if (isNotANumber() == true || tooManyDecimals() == true)
            {
                lblBetResult.Text = "Please enter only the amount you wish to bet.";
            }
            else
            {
                otherThings();
            }
        }
        
        private void otherThings()
        {
            if((double.Parse(lblPlayerMoney.Text.TrimStart('$')) <= 0) 
                && (lblBetResult.Text == "You have no " +
                "more money. Come back later when you have earned some more.<br><br>" +
                "...OR...<br><br> You can hit the button again to get your money back."))
            {
                moreMoney();
            }
            else if (double.Parse(lblPlayerMoney.Text.TrimStart('$')) <= 0)
            {
                lblBetResult.Text = noMoreMoney();
            }
            else
                lblBetResult.Text = notValidBet();
        }

        //check for NaN's
        private bool isNotANumber()
        {
            foreach(char c in txtPlayerBet.Text)
            {
                if (Char.IsDigit(c)) goto yes;
                else goto no;
            }

            no: return true;
            yes: return false;
        }

        //check for multiple decimals
        private bool tooManyDecimals()
        {
            foreach (char c in txtPlayerBet.Text)
            {
                if (tooManyDecimalPoints(c) == true) goto yes;
            }

            return false;
            yes: return true;
            ;
        }

        int decimalCounter = 0;

        private void counter()
        {
            decimalCounter++;
        }
        
        //count decimals
        private bool tooManyDecimalPoints(char c)
        {
            if (!Char.IsDigit(c)) counter();

            if (decimalCounter > 1)
            {
                return true;
            }
            else
                return false;
        }

        //no more money
        private string noMoreMoney()
        {
            return "You have no more money. Come back later when you have earned some more.<br><br>" +
                "...OR...<br><br> You can hit the button again to get your money back.";
        }

        //reset money
        private void moreMoney()
        {
            lblBetResult.Text = "You got your money back!!!";
            lblPlayerMoney.Text = "$100.00";
        }

        //perform if loss
        private void loser()
        {
            if(youLost() == true)
            {
                lblPlayerMoney.Text = loserPlayerMoney();
                lblBetResult.Text = textYouLost();
            }
        }

        //perform if win
        private void winner()
        {
            if(youWon() == true)
            {
                cherryWinnerText();
                sevensWinnerText();
            }
        }

        #region
        //if a cherry winning condition display the winnings
        private void cherryWinnerText()
        {
            if (checkFor1Cherry() == true ||
                    checkFor2Cherries() == true ||
                    checkFor3Cherries() == true)
            {
                lblPlayerMoney.Text = cherryWinnerPlayerMoney();
                lblBetResult.Text = cherryWinnerBetResult();
            }
        }

        //string for cherry winnings
        private string cherryWinnerPlayerMoney()
        {
            return cherryMoneyWon();
        }

        //add cherry winnings to string
        private string cherryWinnerBetResult()
        {
            return textYouWon(cherryWinnings(convertMoneyBet())).ToString();
        }

        //create string to show cherry winnings
        private string cherryMoneyWon()
        {
            double x = convertMoneyBet();
            double y = convertPlayerMoneyToDouble();
            double winnings = cherryWinnings(x);
            x = winnings + y;
            return String.Format("{0:C}", x);
        }

        //calculate cherry winnings
        private double cherryWinnings(double moneyBet)
        {
            if (checkFor3Cherries() == true)
                return threeCherryWinnings(moneyBet);
            else if (checkFor2Cherries() == true)
                return twoCherryWinnings(moneyBet);
            else
                return oneCherryWinnings(moneyBet);
        }
        #endregion

        #region
        //set labels to correct text if 3 7's
        private void sevensWinnerText()
        {
            if (checkFor3Sevens() == true)
            {
                lblPlayerMoney.Text = sevensWinnerPlayerMoney();
                lblBetResult.Text = sevensWinnerBetResult();
            }
        }

        //turn 3 7's winning to string for BetResults
        private string sevensWinnerBetResult()
        {
            return textYouWon(threeSevensWinnings(convertMoneyBet())).ToString();
        }

        //tell user bet is invalid
        private string notValidBet()
        {
                return "That is an invalid bet.";
        }

        //add player money to winnings and convert to string
        private string sevensWinnerPlayerMoney()
        {
            return winnerSevens();
        }

        //string to show 3 7's winnings
        private string winnerSevens()
        {
            double y = convertPlayerMoneyToDouble();
            double x = 0;
            if (checkFor3Sevens() == true)
            {
                x = threeSevensWinnings(convertMoneyBet());
                x = x + y;
            }

            return String.Format("{0:C}", x);
        }
#endregion

        #region
        //create string to show losses
        private string loserPlayerMoney()
        {
            double x = convertMoneyBet();
            playerMoney = playerMoney - x;
            return String.Format("{0:C}", playerMoney);
        }

        //winning string
        private string textYouWon(double moneyWon)
        {
            return String.Format("You bet {0:C} and won {1:C}!", convertMoneyBet(), moneyWon);
        }

        

        //losing text
        private string textYouLost()
        {
            double x = convertMoneyBet();
            string betLostText = "";
            if (youLost() == true)
                betLostText = String.Format("Sorry, you lost {0:C}. Better luck next time.", x);

            return betLostText;    
        }
        #endregion

        #region
        //chekc for bad bets
        //check for invalid negative or $0 bets
        private bool isBetNegative()
        {
            if (convertMoneyBet() <= 0)
                return true;
            else
                return false;
        }

        //see if bet is higher than PlayerMoney
        private bool isBetTooHigh()
        {
            double x = double.Parse(lblPlayerMoney.Text.TrimStart('$'));
            if (convertMoneyBet() > x)
                return true;
            else
                return false;
        }
        #endregion

        #region
        //convert player money and bet
        //convert player money if win or loss
        private double convertPlayerMoneyToDouble()
        {
            if(youWon() == true)
                return convertPlayerMoneyToDoubleWin();
            else
                return convertPlayerMoneyToDoubleLoss();
        }

        //convert playerMoney when win
        private double convertPlayerMoneyToDoubleWin()
        {
            double x;
            string text = lblPlayerMoney.Text;
            x = double.Parse(text.TrimStart('$'));
            x = x - convertMoneyBet();
            return x;
        }

        //convert playerMoney when loss
        private double convertPlayerMoneyToDoubleLoss()
        {
            double x;
            string text = lblPlayerMoney.Text;
            x = double.Parse(text.TrimStart('$'));
            return x;
        }

        //convert bet text to double
        private double convertMoneyBet()
        {
            double x = double.Parse(txtPlayerBet.Text);
            return x;
        }
        #endregion

        #region
        //calculate winnings for winning combos
        //calculate winnings for 1 Cherry
        private double oneCherryWinnings(double bet)
        {
            double x = bet;
                x = x * 2;
            return x;
        }

        //calculate winnings for 2 Cherries
        private double twoCherryWinnings(double bet)
        {
            double x = bet;
                x = x * 3;
            return x;
        }

        //calculate winnings for 3 Cherries
        private double threeCherryWinnings(double bet)
        {
            double x = bet;
                x = x * 4;
            return x;
        }

        //calculate winnings for 3 7's
        private double threeSevensWinnings(double moneyBet)
        {
            double x = moneyBet;
                x = x * 100;
            return x;
        }
        #endregion
        
        #region
        //select random image for each image section
        private void selectImages()
        {
            selectImageOne();
            selectImageTwo();
            selectImageThree();
        }
        
        private void selectImageOne()
        {
            //random pic for #1
            int x = random.Next(0, 11);
            imgOne.ImageUrl = "./Images/" + imgArray[x];
        }

        private void selectImageTwo()
        {
            //random pic for #2
            int x = random.Next(0, 11);
            imgTwo.ImageUrl = "./Images/" + imgArray[x];
        }

        private void selectImageThree()
        {
            //random pic for #3
            int x = random.Next(0, 11);
            imgThree.ImageUrl = "./Images/" + imgArray[x];
        }
#endregion

        #region
        //check for win/lose conditions
        //lose conditions
        private bool youLost()
        {
            if (lostByBar() == true || lostByNone() == true)
                return true;
            else
                return false;
        }

        //win conditions
        private bool youWon()
        {
            if ((checkFor3Sevens() == true ||
                checkFor1Cherry() == true ||
                checkFor2Cherries() == true ||
                checkFor3Cherries() == true) && 
                checkImagesForBar() == false)
                return true;
            else
                return false;
        }

        //found a bar
        private bool lostByBar()
        {
            if (checkImagesForBar() == true)
                return true;
            else
                return false;
        }

        //found no wins
        private bool lostByNone()
        {
            if (checkFor1Cherry() == false &&
                checkFor2Cherries() == false &&
                checkFor3Cherries() == false &&
                checkFor3Sevens() == false)
                return true;
            else
                return false;
        }

        //look for 1 cherry
        private bool checkFor1Cherry()
        {
            if (checkImageOneForCherry() == true ||
                checkImageTwoForCherry() == true ||
                checkImageThreeForCherry() == true)
                return true;
            else
                return false;
        }

        //look for 2 cherries
        private bool checkFor2Cherries()
        {
            if ((checkImageOneForCherry() == true && checkImageTwoForCherry() == true) ||
                (checkImageOneForCherry() == true && checkImageThreeForCherry() == true) ||
                (checkImageTwoForCherry() == true && checkImageThreeForCherry() == true))
                return true;
            else
                return false;
        }

        //look for 3 cherries
        private bool checkFor3Cherries()
        {
            if (checkImageOneForCherry() == true &&
                checkImageTwoForCherry() == true &&
                checkImageThreeForCherry() == true)
                return true;
            else
                return false;
        }

        //see if imgOne is Cherry
        private bool checkImageOneForCherry()
        {
            if (imgOne.ImageUrl == "./Images/Cherry.png")
                return true;
            else
                return false;
        }

        //see if imgTwo is Cherry
        private bool checkImageTwoForCherry()
        {
            if (imgTwo.ImageUrl == "./Images/Cherry.png")
                return true;
            else
                return false;
        }

        //see if imgThree is Cherry
        private bool checkImageThreeForCherry()
        {
            if (imgThree.ImageUrl == "./Images/Cherry.png")
                return true;
            else
                return false;
        }

        //see if all three images are 7's
        private bool checkFor3Sevens()
        {
            if (imgOne.ImageUrl == "./Images/Seven.png" &&
                imgTwo.ImageUrl == "./Images/Seven.png" &&
                imgThree.ImageUrl == "./Images/Seven.png")
                return true;
            else
                return false;
        }

        //check each image for a Bar
        private bool checkImagesForBar()
        {
            if (checkImageOneForBar() == true || 
                checkImageTwoForBar() == true || 
                checkImageThreeForBar() == true)
                return true;
            else
                return false;
        }

        private bool checkImageOneForBar()
        {
            if (imgOne.ImageUrl == "./Images/Bar.png")
                return true;
            else
                return false;
        }

        private bool checkImageTwoForBar()
        {
            if (imgTwo.ImageUrl == "./Images/Bar.png")
                return true;
            else
                return false;
        }

        private bool checkImageThreeForBar()
        {
            if (imgThree.ImageUrl == "./Images/Bar.png")
                return true;
            else
                return false;
        }
        #endregion
    }
}