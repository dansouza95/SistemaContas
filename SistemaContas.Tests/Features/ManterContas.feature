Funcionalidade: Manter conta

@SemLogin
Cenário: Cadastrar uma conta
Dado que estou na página "http://localhost:10941/Acesso/Login"
E preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
E clico na opcao "Entrar"
Então devo ver o elemento "canvas"
E clico na opcao "MenuContas"
E clico na opcao "CadastrarConta"
Então clico na opcao "Credito" 
E preencho o campo "CredorOuDevedor" com o valor "Cliente"
E preencho o campo "Descricao" com o valor "Prestação de serviços"
E preencho o campo "ValorConta" com o valor "150,00"
E preencho o campo "DataVencimento" com o valor "30/10/2014"
E clico na opcao "ValorConta"
Quando clico na opcao "btn_salvar"
Então devo ver o elemento "table"