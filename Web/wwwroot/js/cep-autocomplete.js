// Preenchimento automático de endereço a partir do CEP
// Utiliza a API ViaCEP para buscar informações do endereço

document.addEventListener('DOMContentLoaded', function() {
    const cepInput = document.querySelector('input[name="Cliente.END_CEP"]');
    
    if (cepInput) {
        cepInput.addEventListener('blur', function() {
            const cep = this.value.replace(/\D/g, ''); // Remove caracteres não numéricos
            
            if (cep.length === 8) {
                buscarCEP(cep);
            }
        });
    }
});

function buscarCEP(cep) {
    // Formata o CEP para o padrão XXXXX-XXX
    const cepFormatado = cep.substring(0, 5) + '-' + cep.substring(5);
    
    // Faz a requisição para a API ViaCEP
    fetch(`https://viacep.com.br/ws/${cep}/json/`)
        .then(response => response.json())
        .then(data => {
            if (data.erro) {
                // CEP não encontrado
                limparCampos();
                alert('CEP não encontrado. Verifique o valor digitado.');
                return;
            }
            
            // Preenche os campos com os dados retornados
            preencherCampos(data);
        })
        .catch(error => {
            console.error('Erro ao buscar CEP:', error);
            alert('Erro ao buscar o CEP. Tente novamente.');
        });
}

function preencherCampos(dados) {
    // Preenche o campo de logradouro
    const logradouroInput = document.querySelector('input[name="Cliente.END_LOG"]');
    if (logradouroInput) {
        logradouroInput.value = dados.logradouro || '';
    }
    
    // Preenche o campo de bairro
    const bairroInput = document.querySelector('input[name="Cliente.END_BAI"]');
    if (bairroInput) {
        bairroInput.value = dados.bairro || '';
    }
    
    // Preenche o campo de cidade
    const cidadeInput = document.querySelector('input[name="Cliente.END_CID"]');
    if (cidadeInput) {
        cidadeInput.value = dados.localidade || '';
    }
    
    // Preenche o campo de estado (UF)
    const estadoInput = document.querySelector('input[name="Cliente.END_EST"]');
    if (estadoInput) {
        estadoInput.value = dados.uf || '';
    }
    
    // Foca no campo de número para o usuário completar o endereço
    const numeroInput = document.querySelector('input[name="Cliente.END_NUM"]');
    if (numeroInput) {
        numeroInput.focus();
    }
}

function limparCampos() {
    // Limpa os campos de endereço
    const campos = [
        'Cliente.END_LOG',
        'Cliente.END_BAI',
        'Cliente.END_CID',
        'Cliente.END_EST'
    ];
    
    campos.forEach(campo => {
        const input = document.querySelector(`input[name="${campo}"]`);
        if (input) {
            input.value = '';
        }
    });
}
