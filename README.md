# ServiceCotacao responsável por monitorar uma tabela com dados de moedas origens, moedas destinos e valores, realizar a conversão 
consumindo a API do banco central e enviar o resultado por e-mail para o solicitante. 
Esta tabela é alimentada pelo endpoint InsertQuotation que está na ApiDotNet3_1.
O serviço realiza comunicação com SqlServer através da connectionString vinda de um arquivo de configuração(ConfigSrv), onde também estão
os timers do serviço que irão determinar de quanto em quanto tempo ele irá rodar. 