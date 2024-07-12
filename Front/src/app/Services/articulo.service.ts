import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment'

import { Articulo } from '../Interfaces/articulo';

import { IService } from './i-service';

@Injectable({
  providedIn: 'root'
})
export class ArticuloService implements IService {

  private apiUrl: string = environment.API_URL;
  private endPoint: string = this.apiUrl + 'articulo';

  constructor(private http: HttpClient) { }
  create(model: Articulo): Observable<Articulo> {
    return this.http.post<Articulo>(`${this.endPoint}/`, model);
  }
  read(): Observable<Articulo[]> {
    return this.http.get<Articulo[]>(`${this.endPoint}s/`);
  }
  update(id: number, model: Articulo): Observable<void> {
    return this.http.put<void>(`${this.endPoint}/${id}`, model);
  }
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endPoint}/${id}`);
  }
/*
  getArticulos(): Observable<Articulo[]> {
    return this.http.get<Articulo[]>(`${this.endPoint}s/`);
  }

  addArticulo(model: Articulo): Observable<Articulo> {
    return this.http.post<Articulo>(`${this.endPoint}/`, model);
  }

  updateArticulo(id: number, model: Articulo): Observable<void> {
    return this.http.put<void>(`${this.endPoint}/${id}`, model);
  }

  deleteArticulo(id: number): Observable<void> {
    return this.http.delete<void>(`${this.endPoint}/${id}`);
  }
*/
}
