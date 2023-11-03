// #1 Uputstvo za igru
Console.WriteLine("Dobro dosli u igru iks - oks!");
Console.WriteLine("Znak upisujete unosom broja 1-9 na sledeci nacin:\n");
ExampleBoard();

// #2 Spoljasnji while loop koji sluzi da se udje ili izadje iz igre
while(true)
{
    // #3 Komande za pokretanje i izlazenje iz igre
    string? readResult;
    Console.WriteLine("\nZapcni novu igru (d/n)?");
    readResult = Console.ReadLine();

    if (readResult.ToLower().Trim() == "d")
    {
        Console.WriteLine("\nZapocnimo igru!\n");

        // Matrica stringova sa praznim poljima koja se popunjava sa X ili O
        string[,] board = new string[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                board[i, j] = " ";

        // Matrica integera koja sluzi za proveru pobednika
        int[,] winner = new int[3, 3];
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                winner[i, j] = 0;

        // #4 Unutrasnji while loop kojim biramo mod igre i proveravamo unos
        while (true)
        {
            Console.WriteLine("\nIzaberite mod:");
            Console.WriteLine("1. Player VS Computer\t2. Player Vs Player");
            readResult = Console.ReadLine();

            if (readResult == "1")
            {
                // #5 Proverava unos i odredjuje ko ce prvi igrati
                while (true)
                {
                    Console.WriteLine("\nDa li želite da igrate prvi? (d/n):");
                    readResult = Console.ReadLine();

                    if (readResult.ToLower().Trim() == "d")
                    {
                        // Igrac prvi igra
                        UpdateBoard(board);
                        while (true)
                        {
                            Console.WriteLine("\nUnesite polje:");
                            readResult = Console.ReadLine();

                            // Proverava da li je igrac uneo neki od brojeva 1-9
                            if (ValidateInput(readResult))
                            {
                                // Proverava da li je polje vec zauzeto
                                if (ValidateBoard(board, readResult))
                                {
                                    // Igracev potez
                                    PlayerTurn(board, readResult, winner);
                                    if (CheckWinner(winner))
                                    {
                                        Console.WriteLine();
                                        UpdateBoard(board);
                                        Console.WriteLine("Pobedili ste!");
                                        break;
                                    }
                                    else if (CheckTie(board))
                                    {
                                        Console.WriteLine();
                                        UpdateBoard(board);
                                        Console.WriteLine("Nerešeno!");
                                        break;
                                    }

                                    // Kompjuterov potez
                                    if (FindBestPlace(board, winner))
                                    {
                                        Console.WriteLine();
                                        UpdateBoard(board);
                                        if (CheckWinner(winner))
                                        {
                                            Console.WriteLine("Izgubili ste!");
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        ComputerTurn(board, winner);
                                        Console.WriteLine();
                                        UpdateBoard(board);
                                        if (CheckWinner(winner))
                                        {
                                            Console.WriteLine("Izgubili ste!");
                                            break;
                                        }
                                    }

                                }
                                else
                                    Console.WriteLine("Polje je zauzeto!");
                            }
                            else
                                Console.WriteLine("Unos nije validan!");
                        }
                    }

                    // Kompjuter igra prvi
                    else if (readResult.ToLower().Trim() == "n")
                    {
                        while (true)
                        {
                            Computer2Turn(board, winner);
                            Console.WriteLine();
                            UpdateBoard(board);
                            if (CheckWinner(winner))
                            {
                                Console.WriteLine("Izgubili ste!");
                                break;
                            }

                            else if (CheckTie(board))
                            {
                                Console.WriteLine();
                                UpdateBoard(board);
                                Console.WriteLine("Nerešeno!");
                                break;
                            }

                            bool repeat = true;
                            while (repeat)
                            {
                                Console.WriteLine("\nUnesite polje:");
                                readResult = Console.ReadLine();
                                if (ValidateInput(readResult))
                                {
                                    if (ValidateBoard(board, readResult))
                                    {
                                        repeat = false;
                                        Player2Turn(board, readResult, winner);
                                        if (CheckWinner(winner))
                                        {
                                            Console.WriteLine();
                                            UpdateBoard(board);
                                            Console.WriteLine("Pobedili ste!");
                                            break;
                                        }
                                    }
                                    else
                                        Console.WriteLine("Polje je zauzeto!");
                                }
                                else
                                    Console.WriteLine("Unos nije validan!");
                            }
                        }
                    }
                    else
                        Console.WriteLine("Unos nije validan!");

                    if (CheckWinner(winner) || CheckTie(board))
                        break;
                }
            }

            // Mod Player VS Player
            else if (readResult == "2")
            {
                UpdateBoard(board);
                while (true)
                {
                    Console.WriteLine("\nIgrač 1:");
                    readResult = Console.ReadLine();
                    if (ValidateInput(readResult))
                    {
                        if (ValidateBoard(board, readResult))
                        {
                            PlayerTurn(board, readResult, winner);
                            UpdateBoard(board);
                            if (CheckWinner(winner))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Pobedio je igrač 1!");
                                break;
                            }
                            else if (CheckTie(board))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Nerešeno!");
                                break;
                            }
                        }
                        else
                            Console.WriteLine("Polje je zauzeto!");
                    }
                    else
                        Console.WriteLine("Unos nije validan!");

                    Console.WriteLine("\nIgrač 2:");
                    readResult = Console.ReadLine();
                    if (ValidateInput(readResult))
                    {
                        if (ValidateBoard(board, readResult))
                        {
                            Player2Turn(board, readResult, winner);
                            UpdateBoard(board);
                            if (CheckWinner(winner))
                            {
                                Console.WriteLine();
                                Console.WriteLine("Pobedio je igrač 2!");
                                break;
                            }
                        }
                        else
                            Console.WriteLine("Polje je zauzeto!");
                    }
                    else
                        Console.WriteLine("Unos nije validan!");
                }
            }
            else
                Console.WriteLine("Unos nije validan!");

            if (CheckWinner(winner) || CheckTie(board))
                break;
        }
    }

    else if (readResult.ToLower().Trim() == "n")
    {
        Console.WriteLine("\nVidimo se opet! :D");
        break;
    }

    else
        Console.WriteLine("\nUnos nije validan!");
    
}

// Funkcije
void ExampleBoard()
{
    Console.WriteLine(" 1 | 2 | 3 ");
    Console.WriteLine("---|---|---");
    Console.WriteLine(" 4 | 5 | 6 ");
    Console.WriteLine("---|---|---");
    Console.WriteLine(" 7 | 8 | 9 ");
}


bool ValidateInput(string input)
{
    switch(input)
    {
        case "1": case "2": case "3": case "4": case "5": case "6": case "7": case "8": case "9":
            return true;

        default:
            return false;
         
    }
}

void UpdateBoard(string[,] board)
{
    Console.WriteLine($" {board[0, 0]} | {board[0, 1]} | {board[0, 2]} ");
    Console.WriteLine("---|---|---");
    Console.WriteLine($" {board[1, 0]} | {board[1, 1]} | {board[1, 2]} ");
    Console.WriteLine("---|---|---");
    Console.WriteLine($" {board[2, 0]} | {board[2, 1]} | {board[2, 2]} ");
}

void PlayerTurn(string[,] board, string input, int[,] winner)
{
    int[] array = FindIndex(input);
    int i = array[0];
    int j = array[1];

    board[i, j] = "X";
    winner[i, j] = 1;
}

void Player2Turn(string[,] board, string input, int[,] winner)
{
    int[] array = FindIndex(input);
    int i = array[0];
    int j = array[1];

    board[i, j] = "O";
    winner[i, j] = -1;
}

bool CheckWinner(int[,] winner)
{
    for (int i = 0; i < 3; i++)
    {
        if ((winner[i, 0] + winner[i, 1] + winner[i, 2]) == 3 || (winner[i, 0] + winner[i, 1] + winner[i, 2]) == -3)
            return true;
        else if ((winner[0, i] + winner[1, i] + winner[2, i]) == 3 || (winner[0, i] + winner[1, i] + winner[2, i]) == -3)
            return true;
    }

    if ((winner[0, 0] + winner[1, 1] + winner[2, 2]) == 3 || (winner[0, 0] + winner[1, 1] + winner[2, 2]) == -3)
        return true;
    if ((winner[0, 2] + winner[1, 1] + winner[2, 0]) == 3 || (winner[0, 2] + winner[1, 1] + winner[2, 0]) == -3)
        return true;

    return false;

}

bool ValidateBoard(string[,] board, string input)
{
    int[] array = FindIndex(input);
    int i = array[0];
    int j = array[1];

    if (board[i, j] == " ")
        return true;
    else
        return false;
}

void ComputerTurn(string[,] board, int[,] winner)
{
    Random radnom = new Random();

    while(true)
    {
        int roll = radnom.Next(1, 10);
        string number = roll.ToString();
        int[] array = FindIndex(number);
        int i = array[0];
        int j = array[1];

        if (board[i, j] == " ")
        {
            board[i, j] = "O";
            winner[i, j] = -1;
            break;
        }
    }
}

void Computer2Turn(string[,] board, int[,] winner)
{
    Random radnom = new Random();

    while (true)
    {
        int roll = radnom.Next(1, 10);
        string number = roll.ToString();
        int[] array = FindIndex(number);
        int i = array[0];
        int j = array[1];

        if (board[i, j] == " ")
        {
            board[i, j] = "X";
            winner[i, j] = 1;
            break;
        }
    }
}

bool CheckTie(string[,] board)
{
    bool fullBoard = true;

    for (int i = 0; i < 3; i++)
        for (int j = 0; j < 3; j++)
            if (board[i, j] == " ")
                fullBoard = false;

    if (fullBoard == true)
        return true;
    else
        return false;
}

int[] FindIndex(string input)
{
    int i = 0, j = 0;
    int[] array = new int[2];

    switch (input)
    {
        case "1":
            i = 0;
            j = 0;
            break;

        case "2":
            i = 0;
            j = 1;
            break;

        case "3":
            i = 0;
            j = 2;
            break;

        case "4":
            i = 1;
            j = 0;
            break;

        case "5":
            i = 1;
            j = 1;
            break;

        case "6":
            i = 1;
            j = 2;
            break;

        case "7":
            i = 2;
            j = 0;
            break;

        case "8":
            i = 2;
            j = 1;
            break;

        case "9":
            i = 2;
            j = 2;
            break;
    }

    array[0] = i;
    array[1] = j;

    return array;
}

bool FindBestPlace(string[,] board, int[,] winner)
{
    // Proverava znak 'O'
    for(int i = 0; i < 3; i++)
    {
        // Proverava da li ima dva znaka 'O' po redu i koloni
        if (winner[i, 0] + winner[i, 1] == -2 && winner[i, 2] == 0)
        {
            board[i, 2] = "O";
            winner[i, 2] = -1;
            return true;
        }
        else if (winner[i, 0] + winner[i, 2] == -2 && winner[i, 1] == 0)
        {
            board[i, 1] = "O";
            winner[i, 1] = -1;
            return true;
        }
        else if (winner[i, 1] + winner[i, 2] == -2 && winner[i, 0] == 0)
        {
            board[i, 0] = "O";
            winner[i, 0] = -1;
            return true;
        }
        else if (winner[0, i] + winner[1, i] == -2 && winner[2, i] == 0)
        {
            board[2, i] = "O";
            winner[2, i] = -1;
            return true;
        }
        else if (winner[0, i] + winner[2, i] == -2 && winner[1, i] == 0)
        {
            board[1, i] = "O";
            winner[1, i] = -1;
            return true;
        }
        else if (winner[1, i] + winner[2, i] == -2 && winner[0, i] == 0)
        {
            board[0, i] = "O";
            winner[0, i] = -1;
            return true;
        }
    }
    // Proverava da li ima dva znaka 'O' po glavnoj dijagonali
    if (winner[0, 0] + winner[1, 1] == -2 && winner[2, 2] == 0)
    {
        board[2, 2] = "O";
        winner[2, 2] = -1;
        return true;
    }
    else if (winner[0, 0] + winner[2, 2] == -2 && winner[1, 1] == 0)
    {
        board[1, 1] = "O";
        winner[1, 1] = -1;
        return true;
    }
    else if (winner[1, 1] + winner[2, 2] == -2 && winner[0, 0] == 0)
    {
        board[0, 0] = "O";
        winner[0, 0] = -1;
        return true;
    }
    // Proverava da li ima dva znaka 'O' po sporednoj dijagonali
    else if (winner[0, 2] + winner[1, 1] == -2 && winner[2, 0] == 0)
    {
        board[2, 0] = "O";
        winner[2, 0] = -1;
        return true;
    }
    else if (winner[0, 2] + winner[2, 0] == -2 && winner[1, 1] == 0)
    {
        board[1, 1] = "O";
        winner[1, 1] = -1;
        return true;
    }
    else if (winner[1, 1] + winner[2, 0] == -2 && winner[0, 2] == 0)
    {
        board[0, 2] = "O";
        winner[0, 2] = -1;
        return true;
    }

    // Proverava znak 'X'
    for (int i = 0; i < 3; i++)
    {
        // Proverava da li ima dva znaka 'X' po redu i koloni
        if (winner[i, 0] + winner[i, 1] == 2 && winner[i, 2] == 0)
        {
            board[i, 2] = "O";
            winner[i, 2] = -1;
            return true;
        }
        else if (winner[i, 0] + winner[i, 2] == 2 && winner[i, 1] == 0)
        {
            board[i, 1] = "O";
            winner[i, 1] = -1;
            return true;
        }
        else if (winner[i, 1] + winner[i, 2] == 2 && winner[i, 0] == 0)
        {
            board[i, 0] = "O";
            winner[i, 0] = -1;
            return true;
        }
        else if (winner[0, i] + winner[1, i] == 2 && winner[2, i] == 0)
        {
            board[2, i] = "O";
            winner[2, i] = -1;
            return true;
        }
        else if (winner[0, i] + winner[2, i] == 2 && winner[1, i] == 0)
        {
            board[1, i] = "O";
            winner[1, i] = -1;
            return true;
        }
        else if (winner[1, i] + winner[2, i] == 2 && winner[0, i] == 0)
        {
            board[0, i] = "O";
            winner[0, i] = -1;
            return true;
        }
    }
    // Proverava da li ima dva znaka 'X' po glavnoj dijagonali
    if (winner[0, 0] + winner[1, 1] == 2 && winner[2, 2] == 0)
    {
        board[2, 2] = "O";
        winner[2, 2] = -1;
        return true;
    }
    else if (winner[0, 0] + winner[2, 2] == 2 && winner[1, 1] == 0)
    {
        board[1, 1] = "O";
        winner[1, 1] = -1;
        return true;
    }
    else if (winner[1, 1] + winner[2, 2] == 2 && winner[0, 0] == 0)
    {
        board[0, 0] = "O";
        winner[0, 0] = -1;
        return true;
    }
    // Proverava da li ima dva znaka 'O' po sporednoj dijagonali
    else if (winner[0, 2] + winner[1, 1] == 2 && winner[2, 0] == 0)
    {
        board[2, 0] = "O";
        winner[2, 0] = -1;
        return true;
    }
    else if (winner[0, 2] + winner[2, 0] == 2 && winner[1, 1] == 0)
    {
        board[1, 1] = "O";
        winner[1, 1] = -1;
        return true;
    }
    else if (winner[1, 1] + winner[2, 0] == 2 && winner[0, 2] == 0)
    {
        board[0, 2] = "O";
        winner[0, 2] = -1;
        return true;
    }

    return false;
}