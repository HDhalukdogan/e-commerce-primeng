import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

export const authGuard: CanActivateFn = (route, state) => {
    let router = inject(Router);
  let authLocalStorageToken = `token`;
  let token = localStorage.getItem(authLocalStorageToken);

  if (token) 
      return true;

  localStorage.removeItem(authLocalStorageToken);
  router.navigate(['/']);
  return false;
};
