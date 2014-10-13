Funcionalidade: Pagamento e recebimento de contas

@SemLogin
Cenário: Pagar uma conta
Dado que estou na página "http://localhost:10941/Acesso/Login"
E preencho o campo "Usuario" com o valor "dansouza.95"
E preencho o campo "Senha" com o valor "01020300"
E clico na opcao "Entrar"
Então devo ver o elemento "canvas"
E clico na opcao "MenuPendencias"
E clico na opcao "EmAberto"
E clico na opcao "Prestação de serviços"
E clico na opcao "Confirmar"
Então devo ver o elemento "table"
E clico na opcao "btn_modal"
E clico na opcao "Prestação de serviços"