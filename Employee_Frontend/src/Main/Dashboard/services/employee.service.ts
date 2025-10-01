import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {


  constructor(private http: HttpClient) { }

  addEmployee(data: any): Observable<any> {
  return this.http.post(`${environment.employeeUrl}/AddEmployee`, data);
}

updateEmployee(id: number,data:any):Observable<any>{
  return this.http.put(`${environment.employeeUrl}/EditEmployee/${id}`, data, { headers: { 'Content-Type': 'application/json-patch+json' }});
}

getEmployee():Observable<any>{
return this.http.get(`${environment.employeeUrl}/GetAllEmployee`);
}

deleteEmployee(id:number):Observable<any>{
  return this.http.delete(`${environment.employeeUrl}/DeleteEmployee/${id}`);
}
}
