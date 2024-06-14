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

    Console.WriteLine($"Result: {result}");
}
TestStraight();


void TestFlush()
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

    Console.WriteLine($"Result: {result}");
}
TestFlush();


Console.ReadKey();