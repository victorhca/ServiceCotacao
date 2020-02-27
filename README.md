# ServiceCotacao serviço que fica instalado no SO, responsável por monitorar uma tabela com dados de moedas e realizar as conversões.
Consumindo a API do banco central captura o resultado e envia por e-mail para o solicitante. 
Esta tabela é alimentada pelo endpoint InsertQuotation que está na ApiDotNet3_1.
O serviço realiza comunicação com SqlServer através da connectionString vinda de um arquivo de configuração(ConfigSrv), onde também estão
os timers do serviço que irão determinar de quanto em quanto tempo ele irá rodar. 
