namespace EnadeProject.Model.Filter.Support
{
    public enum Criterio
    {
        Igual = 0,
        Contem = 1,
        IniciaCom = 2,
        TerminaCom = 4,
        MaiorQue = 8,
        MenorQue = 16,
        MaiorOuIgual = 32,
        MenorOuIgual = 64,
        EstaEntre = 128
    }
}