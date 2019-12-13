using System;
using System.Collections.Generic;

namespace Meta.Desafio.Logica
{
   class Program
   {
      static void Main(string[] args)
      {
         Questao01();
         Questao02();
         Questao03();
         Questao04();

         Console.WriteLine("Teste finalizado...");
         Console.ReadLine();
      }

      /// <summary>
      ///    Dado um array de números inteiros, retorne os índices dos dois números de forma que eles se somem a um alvo específico.
      ///    Você pode assumir que cada entrada teria exatamente uma solução, e você não pode usar o mesmo elemento duas vezes.
      ///    Exemplo:
      ///       Dado nums = [2, 7, 11, 15], alvo = 9,
      ///       Como nums[0] + nums[1] = 2 + 7 = 9,
      ///       return [0, 1].
      /// </summary>
      private static void Questao01()
      {
         int[] listNumbers = { 2, 4, 9, 12, 18, 24 };

         Console.WriteLine("Lista de número para o teste: [2, 4, 9, 12, 18, 24]");

         int target = 0;
         bool targetFound = false;
         string waitExecute = string.Empty;

         do
         {
            do
            {
               Console.WriteLine("Escolha um alvo:");
               var selectedTarget = Console.ReadLine();

               if (int.TryParse(selectedTarget, out target))
               {
                  target = int.Parse(selectedTarget);
               }
               else
               {
                  Console.WriteLine("Alvo inválido... Tente novamente...");
               }
            } while (target == 0);

            for (int x = 0; x < listNumbers.Length; x++)
            {
               for (int y = x + 1; y < listNumbers.Length; y++)
               {
                  if ((listNumbers[x] + listNumbers[y]) == target)
                  {
                     Console.WriteLine($"Alvo encontrado: [{x}, {y}]");
                     targetFound = true;
                  }
               }
            }

            if (!targetFound) Console.WriteLine("Alvo não encontrado");

            Console.WriteLine("Deseja tentar outro alvo? [y/n]");
            waitExecute = Console.ReadLine();
            targetFound = false;
         } while (waitExecute.ToLower() == "y");

         Console.WriteLine($"-------------------------------------------------------------------------");
      }

      /// <summary>
      ///    Um bracket é considerado qualquer um dos seguintes caracteres: (, ), {, }, [ ou ].
      ///    Dois brackets são considerados um par combinado se o bracket de abertura(isto é, (, [ou {) ocorre à esquerda de um
      ///    bracket de fechamento(ou seja,),] ou} do mesmo tipo exato.Existem três tipos de pares de brackets : [], {} e().
      ///    Um par de brackets correspondente não é balanceado se o de abertura e o de fechamento não corresponderem entre si.Por exemplo, {[(])} não é balanceado porque o conteúdo entre {e}
      ///    não é balanceado.O primeiro bracket inclui o de abertura, (, e o segundo inclui um bracket de fechamento desbalanceado,].
      ///    Dado sequencias de caracteres, determine se cada sequência de brackets é balanceada.Se uma string estiver balanceada, retorne SIM.Caso contrário, retorne NAO.
      ///    Exemplo:
      ///       {[()]} SIM
      ///       {[(])} NAO
      ///       {{[[(())]]}} SIM
      /// </summary>
      private static void Questao02()
      {
         string[] bracketList = { "{[()]}", "{[(])}", "{{[[(())]]}}" };
         bool isCorret = true;

         for (int i = 0; i < bracketList.Length; i++)
         {
            string bracketTest = bracketList[i];

            if (bracketTest.Length % 2 == 0)
            {
               for (int x = 0; x < (bracketTest.Length / 2) - 1; x++)
               {
                  char openBracket = bracketTest[x];
                  char closeBracket = bracketTest[bracketTest.Length - (x + 1)];

                  isCorret = (openBracket == '(' && closeBracket == ')' || openBracket == '[' && closeBracket == ']' || openBracket == '{' && closeBracket == '}');
                  if (!isCorret) break;
               }

               Console.WriteLine($"O bracket '{bracketTest}' é válido? {(isCorret ? "SIM" : "NÃO")}");
            }
         }

         Console.WriteLine($"-------------------------------------------------------------------------");
      }

      /// <summary>
      ///    Digamos que você tenha um array para o qual o elemento i é o preço de uma determinada ação no dia i.
      ///    Se você tivesse permissão para concluir no máximo uma transação (ou seja, comprar uma e vender uma ação), crie um algoritmo para encontrar o lucro máximo.
      ///    Note que você não pode vender uma ação antes de comprar.
      ///    
      ///    Exemplo:
      ///      Input: [7,1,5,3,6,4]
      ///      Output: 5 (Comprou no dia 2 (preço igual a 1) e vendeu no dia 5 (preço igual a 6), lucro foi de 6 – 1 = 5
      ///      
      ///      Input: [7,6,4,3,1]
      ///      Output: 0 (Nesse caso nenhuma transação deve ser feita, lucro máximo igual a 0)
      /// </summary>
      private static void Questao03()
      {
         List<int[]> samples = new List<int[]>();

         samples.Add(new int[] { 7, 1, 5, 3, 6, 4 });
         samples.Add(new int[] { 7, 6, 4, 3, 1 });

         for (int index = 0; index < samples.Count; index++)
         {
            int currentProfit = 0;
            int maximumProfit = 0;
            int finalPurchaseDay = 0;
            int finalSaleDay = 0;
            int finalPurchaseValue = 0;
            int finalSaleValue = 0;

            for (int purchaseDay = 0; purchaseDay < samples[index].Length; purchaseDay++)
            {
               for (int saleDay = purchaseDay; saleDay < samples[index].Length; saleDay++)
               {
                  currentProfit = samples[index][saleDay] - samples[index][purchaseDay];

                  if (currentProfit > maximumProfit)
                  {
                     finalPurchaseDay = purchaseDay + 1;
                     finalSaleDay = saleDay + 1;
                     finalPurchaseValue = samples[index][purchaseDay];
                     finalSaleValue = samples[index][saleDay];
                     maximumProfit = currentProfit;
                  }
               }
            }

            Console.WriteLine($"{maximumProfit} (Comprou no dia {finalPurchaseDay} (preço igual a {finalPurchaseValue}) e vendeu no dia {finalSaleDay} (preço igual a {finalSaleValue}), lucro foi de {finalSaleValue} – {finalPurchaseValue} = {maximumProfit}");
         }

         Console.WriteLine($"-------------------------------------------------------------------------");
      }

      /// <summary>
      ///    Dados n inteiros não negativos representando um mapa de elevação onde a largura de cada barra é 1, calcule quanta água é capaz de reter após a chuva.
      /// </summary>
      private static void Questao04()
      {
         int[] barElevation = { 0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1 };
         int currentElevation = 0;
         int previousElevation = 0;
         int totalWater = 0;

         for (int i = 0; i < barElevation.Length; i++)
         {
            currentElevation = barElevation[i];
            if (currentElevation < previousElevation) totalWater += previousElevation - currentElevation;
            previousElevation = currentElevation;
         }

         Console.WriteLine($"Quantidade de água retida: {totalWater}");
         Console.WriteLine($"-------------------------------------------------------------------------");
      }
   }
}