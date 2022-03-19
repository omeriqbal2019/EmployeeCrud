import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';

import { AccountService } from '@app/_services';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    employeesDataset = null;

    constructor(private accountService: AccountService) {}

    ngOnInit() {

        this.accountService.getAll()
            .pipe(first())
            .subscribe( employees => {
                this.employeesDataset = employees
            });
    }

    deleteEmployee(id: string) {
        const user = this.employeesDataset.find(x => x.employeeId === id);
        user.isDeleting = true;
        this.accountService.delete(id)
            .pipe(first())
            .subscribe(() => {
                this.employeesDataset = this.employeesDataset.filter(x => x.employeeId !== id) 
              
            });
    }
}