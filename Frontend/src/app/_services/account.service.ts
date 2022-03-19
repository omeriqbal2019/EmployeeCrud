import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { Employee } from '@app/_models';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<Employee>;
    public employee: Observable<Employee>;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject<Employee>(JSON.parse(localStorage.getItem('employee')));
        this.employee = this.userSubject.asObservable();
    }

    public get userValue(): Employee {
        return this.userSubject.value;
    }

    login(username, password) {
  
        return this.http.post<Employee>(`${environment.apiUrl}/Auth/Login`, { username, password })
            .pipe(map(employee => {
                localStorage.setItem('employee', JSON.stringify(employee));
                this.userSubject.next(employee);
                return employee;
            }));
    }

    logout() {
        localStorage.removeItem('employee');
        this.userSubject.next(null);
        this.router.navigate(['/account/login']);
    }

    register(employee: Employee) {
        return this.http.post(`${environment.apiUrl}/Employee/RegisterEmployee`, employee);
    }

    getAll() {
        return this.http.get<Employee[]>(`${environment.apiUrl}/Employee/GetAllEmployeesList`);
    }

    getById(id: string) {
        var empid=parseInt(id);
        return this.http.get<Employee>(`${environment.apiUrl}/Employee/GetEmployeebyId?empid=${empid}`);
    }

    update(id, params) {
        return this.http.put(`${environment.apiUrl}/Employee/UpdateEmployeeInfo?id=${id}`, params)
            .pipe(map(x => {
                if (id == this.userValue.employeeId) {
                    const employee = { ...this.userValue, ...params };
                    localStorage.setItem('employee', JSON.stringify(employee));
                    this.userSubject.next(employee);
                }
                return x;
            }));
    }

    delete(id: string) {
        return this.http.delete(`${environment.apiUrl}/Employee/DeleteEmployee?employeeId=${id}`)
            .pipe(map(x => {
                if (id == this.userValue.employeeId) {
                    this.logout();
                }
                return x;
            }));
    }
}