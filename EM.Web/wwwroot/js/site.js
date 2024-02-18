const inputCpf = document.querySelector('#cpf');
const inputNasc = document.querySelector('#nasc');
const inputNome = document.querySelector('#nome');

function validaCampoNome() {
    const nome = document.querySelector('nome');
    console.log(nome.length);
    if (nome.value.trim() == "" || nome.length < 3) {
        Swal.fire({
            icon: 'error',
            title: 'Verifique os dados digitados!',
            showConfirmButton: false,
            timer: 1500
        })
        console.log(nome.length);
        setTimeout(function () { $('#nome').focus(); }, 1);
        return false

    }
}
function validaCampoMatricula() {
    const matricula = document.querySelector('matricula');
    if (matricula.value.trim() == "" || matricula.value < 1) {
        Swal.fire({
            type: type,
            title: title,
            text: mensage,
            icon: 'warning',
            showConfirmeButton: false,
            timer: 1500,
        })
        setTimeout(function () { $('#matricula').focus(); }, 1);
        return false
    }
}

inputCpf.addEventListener('keypress', () => {
    let inputlength = inputCpf.value.length
    if (inputlength === 3 || inputlength === 7) {
        inputCpf.value += '.';
    } else if (inputlength === 11) {
        inputCpf.value += '-';
    }
})

inputNasc.addEventListener('keypress', () => {
    let inputlength = inputNasc.value.length
    if (inputlength === 2 || inputlength === 5) {
        inputNasc.value += '/';
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

function validadata(control) {
    var data = document.getElementById("nasc").value;
    data = data.replace(/\//g, "-"); 
    var data_array = data.split("-");
    
    if (data_array[0].length != 4) {
        data = data_array[2] + "-" + data_array[1] + "-" + data_array[0];
    }
};

function onlynumber(evt) {
    var theEvent = evt || window.event;
    var key = theEvent.keyCode || theEvent.which;
    key = String.fromCharCode(key);
    
    var regex = /^[0-9]+$/;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
};

    function confirmDeletar() {
        Swal.fire({
            title: 'Quer mesmo deletar?',
            text: 'Você não poderá reverter essa ação!',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
        }).then((result) => {
            // Se o usuário confirmar, chama a função deletar
            if (result.isConfirmed) {
                deletar();
            }
        });
    // Retorna false para evitar a execução da ação padrão do link
    return false;
                                }

function deletar(a)
{
    if (result.value == true)
    {
            swal(
                'Deletado!',
                'Deletado com Sucesso!',
                'success'

            )
        }
    })
};
