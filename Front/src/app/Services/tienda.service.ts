import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'

import { Tienda } from '../Interfaces/tienda';

import { IService } from './i-service';
import { IModel } from '../Interfaces/i-model';

@Injectable({
  providedIn: 'root'
})
export class TiendaService implements IService {

  private apiUrl: string = environment.API_URL;
  private endPoint: string = this.apiUrl + 'tienda';

  constructor(private http: HttpClient) { }

  create(model: Tienda): Observable<Tienda> {
    return this.http.post<Tienda>(`${this.endPoint}/`, model);
  }

  read(): Observable<Tienda[]> {
    return this.http.get<Tienda[]>(`${this.endPoint}s/`);
  }

  update(id: number, model: Tienda): Observable<void> {
    return this.http.put<void>(`${this.endPoint}/${id}`, model);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endPoint}/${id}`);
  }
}
