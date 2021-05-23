# Beacon Tracker
Descricao do funcionamento do sistema
###Enviar notificacao para o app
    a. Admin aciona funcao da Api
    b. Chamar one signal enviar msg para o app
    c. App recebe notificacao

###Listar mapeamento frigorifico
    a. Puxar da Api os parametros de limites do frigorifico
    b. Puxar a cada x tempo os dados do stream do Kafka (live)
    c. Colocar cada pessoa no seu respectivo lugar (tabela)

###Listar historico usuario
    a. Chamar do bd o historico

###Solicitar entrada na regiao
    a. Solicitar entrada no lab (App)
    b. Inscrever nova pessoa no kafka
    c. Iniciar contagem de tempo para a regiao entrado
    d. Notificar caso ultrapassar do tempo maximo do lugar

###Enviar RSSI
    a. Rotina de envio de RSSI com nome da pessoa, producer (App pro Kafka)
    b. Mantem a stream
    c. Fazer upload no banco de dados periodicamente