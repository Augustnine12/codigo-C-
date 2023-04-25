class Guloso {
    static int vertice; // numero de vertices
    static int [] caminho; // melhor caminho 
    static int[,] distanciaV; // distancia entre as vertices
    static int melhorCusto = int.MaxValue;
    static void Main(string[] args) {    
        string arquivoEntrada = args[0];
        string arquivoSaida = args[1];

        lerArquivo(arquivoEntrada);

        int[] caminhoAtual = new int[vertice];
        bool[] visitado = new bool[vertice];

        caminhoAtual[0] = 1;
        visitado[0] = true;

        buscarCaminho(caminhoAtual, visitado, 1, 0);
        mostrarSaida(arquivoSaida);
    }

    static void lerArquivo(string arquivo) {
        string[] linha = File.ReadAllLines(arquivo);
        int tamanho = linha.Length;
        distanciaV = new int[tamanho, tamanho];
        for (int i = 0; i < tamanho; i++) {
            string[] value = linha[i].Split(' ');
            for (int j = 0; j < tamanho; j++)
            {
                distanciaV[i, j] = int.Parse(value[j]);
            }
        }
        vertice = tamanho;
    }

    static void buscarCaminho(int[] caminhoAtual, bool[] visitado, int level, int custoAtual) {
        int cidadeAtual = caminhoAtual[level - 1] - 1;
        int cidadeMaisProxima = -1;
        int distanciaMaisProxima = int.MaxValue;
        for (int i = 0; i < vertice; i++) {
            if (!visitado[i] && distanciaV[cidadeAtual, i] < distanciaMaisProxima) {
                cidadeMaisProxima = i;
                distanciaMaisProxima = distanciaV[cidadeAtual, i];
            }
        }
        if (cidadeMaisProxima != -1) {
            caminhoAtual[level] = cidadeMaisProxima + 1;
            visitado[cidadeMaisProxima] = true;
            buscarCaminho(caminhoAtual, visitado, level + 1, custoAtual + distanciaMaisProxima);
            visitado[cidadeMaisProxima] = false;
        }

        if (level == vertice) {
            int custo = custoAtual + distanciaV[caminhoAtual[level - 1] - 1, 0]; 
            if (custo < melhorCusto) {
                caminho = caminhoAtual;
                melhorCusto = custo;
            }
            return;
        }
    }
    static void mostrarSaida(string arquivo) {
        using (StreamWriter saida = new StreamWriter(arquivo)) {
            if (caminho != null) {
                foreach (int vertice in caminho) {
                    saida.WriteLine(vertice);
                }
            }
            saida.WriteLine(melhorCusto);
        }
    }

}