using Newtonsoft.Json;

namespace desafio_backend.Models
{
    public class CardBody
    {
        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        [JsonProperty("conteudo")]
        public string Conteudo { get; set; }

        [JsonProperty("lista")]
        public string Lista { get; set; }

    }
}
