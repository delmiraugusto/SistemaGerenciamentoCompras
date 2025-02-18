import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, map } from 'rxjs';
import { Constants } from './Constants';
import { Product, ProductInsert, ProductUpdate } from '../models/Product';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) { }

  GetList = () =>
    this.http.get<Product[]>(`${Constants.PRODUCT}/Read`).pipe(
      map((products: Product[]) => products),
      catchError(error => { throw error; })
    );

  GetById = (id: number) =>
    this.http.get<Product>(`${Constants.PRODUCT}/Read/${id}`).pipe(
      map((product: Product) => product),
      catchError(error => { throw error; })
    );

  Insert = (obj: ProductInsert) =>
    this.http.post(`${Constants.PRODUCT}/Create`, obj).pipe(
      catchError(error => { throw error; })
    );

  UpdateById = (id: number, obj: ProductUpdate) =>
    this.http.put(`${Constants.PRODUCT}/UpdateById/${id}`, obj).pipe(
      catchError(error => { throw error; })
    );

  RemoveById = (id: number) =>
    this.http.delete(`${Constants.PRODUCT}/DeleteById/${id}`).pipe(
      catchError(error => { throw error; })
    );
}
