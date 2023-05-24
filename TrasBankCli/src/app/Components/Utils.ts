import Swal from "sweetalert2";

export const alertLoading: any = Swal.mixin({
  title: "Loading...",
  icon: 'info',
  showConfirmButton: false,
  allowOutsideClick: false
});
export function errorAlert(val: string) {
  Swal.fire({
    icon: "error",
    title: val
  })
}
