import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandlerFn,
  HttpInterceptorFn,
  HttpEvent,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { CookieService } from 'ngx-cookie-service';
import { inject } from '@angular/core';

export const authInterceptor: HttpInterceptorFn = (
  httpRequest: HttpRequest<unknown>,
  next: HttpHandlerFn
): Observable<HttpEvent<unknown>> => {
  const cookieService = inject(CookieService);

  if (shouldInterceptRequest(httpRequest)) {
    // Récupérer le token et vérifier qu'il existe
    const token = cookieService.get('Authorization');
    console.log('Token récupéré:', token); // Pour déboguer
    
    if (!token) {
      console.error('Token d\'autorisation manquant dans les cookies');
      return next(httpRequest); // Continuer sans modification
    }
    
    // S'assurer que le token a le format "Bearer [token]"
    const authValue = token.startsWith('Bearer ') ? token : `Bearer ${token}`;
    
    // Utiliser un objet HttpHeaders plutôt que setHeaders
    const headers = httpRequest.headers.set('Authorization', authValue);
    
    const authRequest = httpRequest.clone({ headers });
    
    // Vérifier que l'en-tête a bien été ajouté
    console.log('En-tête Authorization ajouté:', authRequest.headers.get('Authorization'));
    
    return next(authRequest);
  }  
  return next(httpRequest);
};

function shouldInterceptRequest(request: HttpRequest<any>): boolean {
  return request.urlWithParams.indexOf('addAuth=true', 0) > -1;
}
