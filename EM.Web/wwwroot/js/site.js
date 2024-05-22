const inputCpf = document.querySelector('#cpf');

inputCpf.addEventListener('keypress', () => {
    let inputlength = inputCpf.value.length

    if (inputlength === 3 || inputlength === 7) {
        inputCpf.value += '.';
    } else if (inputlength === 11) {
        inputCpf.value += '-';
    }
})

function verificarCPF(c) {
    var cpfOriginal = c;
    if (c.length === 14) {
        c = c.replace(/[^\d]+/g, '');
        var baseCpf = c;
        var v = false;
        var i;
        s = c;
        var c = s.substr(0, 9);
        var dv = s.substr(9, 2);
        var d1 = 0;

        if (
            baseCpf == "00000000000" ||
            baseCpf == "11111111111" ||
            baseCpf == "22222222222" ||
            baseCpf == "33333333333" ||
            baseCpf == "44444444444" ||
            baseCpf == "55555555555" ||
            baseCpf == "66666666666" ||
            baseCpf == "77777777777" ||
            baseCpf == "88888888888" ||
            baseCpf == "99999999999") {
            alert("CPF " + cpfOriginal + "  Inválido");
            v = true;
            setTimeout(function () { $('#cpf').focus(); }, 1);
            return false;
        }

        for (i = 0; i < 9; i++) {
            d1 += c.charAt(i) * (10 - i);
        }
        if (d1 == 0) {
            alert("CPF " + cpfOriginal + " Inválido");
            v = true;
            setTimeout(function () { $('#cpf').focus(); }, 1);
            return false;
        }
        d1 = 11 - (d1 % 11);
        if (d1 > 9) d1 = 0;
        if (dv.charAt(0) != d1) {
            alert("CPF " + cpfOriginal + " Inválido");
            v = true;
            setTimeout(function () { $('#cpf').focus(); }, 1);
            return false;
        }

        d1 *= 2;
        for (i = 0; i < 9; i++) {
            d1 += c.charAt(i) * (11 - i);
        }
        d1 = 11 - (d1 % 11);
        if (d1 > 9) d1 = 0;
        if (dv.charAt(1) != d1) {
            alert("CPF " + cpfOriginal + " Inválido");

            v = true;
            setTimeout(function () { $('#cpf').focus(); }, 1);
            return false;
        }
        if (!v) {
            console.log("Cpf valido: " + cpfOriginal);
            return true;
        }
    }
}




function validaCampoMatricula() {
    const nomeInput = document.getElementById('nome');
    const nomeValue = nomeInput.value.trim();
    const matriculaInput = document.getElementById('matricula');
    const matriculaValue = matriculaInput.value.trim();

    if (nomeValue == '') {

        matriculaInput.removeAttribute('readonly');
    }
    else {

        matriculaInput.setAttribute('readonly', true);
    }

}

function buscaCEP()
{
    var cep = document.getElementById('cep').value;
    cep = cep.replace(/\D/g, ''); // Remove caracteres não numéricos
    if (cep.length != 8)
    {
     return;
    }
    var xhr = new XMLHttpRequest();
    xhr.open('GET', 'https://viacep.com.br/ws/' + cep + '/json/');
    xhr.onreadystatechange = function ()
    {
        if (xhr.readyState === 4 && xhr.status === 200)
        {
                                var endereco = JSON.parse(xhr.responseText);
            if (!endereco.erro)
            {
                 document.getElementById('logradouro').value = endereco.logradouro;
                 document.getElementById('bairro').value = endereco.bairro;
                 document.getElementById('cidade').value = endereco.localidade;
                 document.getElementById('uf').value = endereco.uf;
            } else
            {
                alert('CEP não encontrado');
            }
        }
    };
  xhr.send();
}
