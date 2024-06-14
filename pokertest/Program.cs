using blazorwasmpoker;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var gameModel = new GameModel();
gameModel.Start();


void TestStraight()
{
    var hand = new List<PlayingCardModel>()
    {
        new PlayingCardModel(52),
        new PlayingCardModel(50),
        new PlayingCardModel(36),
        new PlayingCardModel(25),
        new PlayingCardModel(27)
    };

    gameModel._hand = hand;


    WinCondition result = gameModel.calculateScore();

    Console.WriteLine($"Straight Result: {result}");
}
TestStraight();


void TestFlush()
{
    var hand = new List<PlayingCardModel>()
    {
        new PlayingCardModel(44),
        new PlayingCardModel(13),
        new PlayingCardModel(47),
        new PlayingCardModel(45),
        new PlayingCardModel(43)
    };

    gameModel._hand = hand;


    WinCondition result = gameModel.calculateScore();

    Console.WriteLine($"Flush Result: {result}");
}
TestFlush();


Console.ReadKey();