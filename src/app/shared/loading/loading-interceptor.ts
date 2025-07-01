import { HttpContextToken, HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs';
import { LoadingService } from './loading.service';

export const SkipLoadingHttpContextToken = new HttpContextToken<boolean>(() => false);

export const loadingInterceptorFn: HttpInterceptorFn = (req, next) => {

  if (req.context.get(SkipLoadingHttpContextToken)) {
    return next(req);
  }

  let loadingService = inject(LoadingService);
  loadingService.loadingOn();

  return next(req).pipe(
    finalize(() => {
      loadingService.loadingOff();
    })
  );
};

