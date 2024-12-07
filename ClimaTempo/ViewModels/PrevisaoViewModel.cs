using ClimaTempo.Models;
using ClimaTempo.Services;
using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClimaTempo.ViewModels
{
    public partial class PrevisaoViewModel : ObservableObject
    {
        [ObservableProperty]
        private string cidade;
        [ObservableProperty]
        private string estado;
        [ObservableProperty]
        private string atualizado_Em;
        [ObservableProperty]
        private DateTime data;
        [ObservableProperty]
        private string condicao;
        [ObservableProperty]
        private string condicao_Desc;
        [ObservableProperty]
        private double max;
        [ObservableProperty]
        private double min;
        [ObservableProperty]
        private double indiceuv;
        [ObservableProperty]
        private List<Clima> proximosDias;

        private Previsao previsao;

        private Previsao proxPrevisao;

        [ObservableProperty]
        private string cidade_pesquisada;

        [ObservableProperty]
        private Cidade cidadeSelecionada;

        [ObservableProperty]
        private List<Cidade> cidade_list;


        public ICommand BuscarPrevisaoCommand{ get; }
        public ICommand BuscarCidadesCommand { get; }

        public ICommand BuscarCidadeSelecionadaCommand { get; }

        public PrevisaoViewModel()
        {
            //BuscarPrevisaoCommand = new Command(BuscarPrevisao);
            BuscarCidadesCommand = new Command(BuscarCidades);
            BuscarPrevisaoCommand = new Command<int>(BuscarPrevisao);
            //BuscarCidadeSelecionadaCommand = new Command(BuscarPrevisaoCidade);
        }
        
        public async void BuscarPrevisao(int id)
        {
            //Busca os dados da previsão de uma cidade especificada
            previsao = await new PrevisaoService().GetPrevisaoById(id);

            Max = previsao.Clima[0].Max;
            Min = previsao.Clima[0].Min;

            //Busca a previsão dos próximos 3 dias
            proxPrevisao = await new PrevisaoService().GetPrevisaoForXDaysById(244, 3);
            ProximosDias = proxPrevisao.Clima;
        }
        

        public async void BuscarCidades()
        {
            Cidade_list = new List<Cidade>();
            Cidade_list = await new CidadeService().GetCidadesByName(Cidade_pesquisada);
        }
        /*
        public async void BuscarPrevisaoCidade()
        {
            //Busca os dados da previsão de uma cidade especificada
            previsao = await new CidadeService().GetPrevisaoCidade(cidadeSelecionada.Id);
            Estado = previsao[0].Estado;
            Cidade = previsao[0].Cidade;
            Atualizado_Em = previsao[0].Atualizado_Em.ToString();
            Data = previsao[0].Clima[0].Data;
            Condicao = previsao[0].Clima[0].Condicao;
            Condicao_Desc = previsao[0].Clima[0].Condicao_desc;
            Max = previsao[0].Clima[0].Max;
            Min = previsao[0].Clima[0].Min;

            /*
            //Busca a previsão dos próximos 3 dias
            proxPrevisao = await new PrevisaoService().GetPrevisaoForXDaysById(244, 3);
            ProximosDias = proxPrevisao.Clima;
          
        }
          */




    }
}
