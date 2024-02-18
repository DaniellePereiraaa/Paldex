using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class AlphanumericComparer : IComparer<string>
{
    public int Compare(string x, string y)
    {
        // Implementação de comparação alfanumérica
        // Esta implementação compara os números em uma string como inteiros e os caracteres restantes como strings
        // Isso garante que "10" venha depois de "9" e antes de "11"
        // Você pode usar uma implementação diferente, dependendo dos seus requisitos

        // Converte as strings para arrays de caracteres
        char[] charsX = x.ToCharArray();
        char[] charsY = y.ToCharArray();

        int indexX = 0;
        int indexY = 0;

        while (indexX < charsX.Length && indexY < charsY.Length)
        {
            // Verifica se os caracteres atuais são dígitos numéricos
            bool isDigitX = char.IsDigit(charsX[indexX]);
            bool isDigitY = char.IsDigit(charsY[indexY]);

            if (isDigitX && isDigitY)
            {
                // Se ambos são dígitos numéricos, compara-os como números inteiros
                string numStringX = "";
                while (indexX < charsX.Length && char.IsDigit(charsX[indexX]))
                {
                    numStringX += charsX[indexX++];
                }
                string numStringY = "";
                while (indexY < charsY.Length && char.IsDigit(charsY[indexY]))
                {
                    numStringY += charsY[indexY++];
                }

                int numX = int.Parse(numStringX);
                int numY = int.Parse(numStringY);

                if (numX != numY)
                {
                    return numX.CompareTo(numY);
                }
            }
            else
            {
                // Se os caracteres atuais não são dígitos numéricos, compara-os como caracteres normais
                if (charsX[indexX] != charsY[indexY])
                {
                    return charsX[indexX].CompareTo(charsY[indexY]);
                }
                indexX++;
                indexY++;
            }
        }

        // Se uma string for um prefixo da outra, a string mais curta vem primeiro
        return charsX.Length - charsY.Length;
    }
}
