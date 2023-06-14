import Swal, { SweetAlertIcon } from "sweetalert2";

export function alertLoading(val: string) {
  Swal.fire({
    title: `${val}...`,
    icon: 'info',
    showConfirmButton: false,
    allowOutsideClick:false
  })
};

export function errorAlert(val: string) {
  Swal.fire({
    icon: "error",
    text:val
  })
};

export function successAlert(val: string) {
  Swal.fire({
    icon: "success",
    title: val,
    timer: 2000,
    timerProgressBar: true
  })
}
export function toast(val: string, ico: SweetAlertIcon) {
  Swal.fire({
    toast: true,
    position: "top-right",
    text: val,
    timer: 2000,
    icon:ico,
    timerProgressBar: true,
    showConfirmButton:false
  })
}
