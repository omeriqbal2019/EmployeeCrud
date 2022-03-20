import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { AccountService, AlertService } from '@app/_services';
import { ResponseModel } from '@app/_models/response-model';
@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
    form: FormGroup;
    id: string;
    isAddMode: boolean;
    loading = false;
    submitted = false;

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private accountService: AccountService,
        private alertService: AlertService
    ) {}

    ngOnInit() {
        this.id = this.route.snapshot.params['id'];
        this.isAddMode = !this.id;
        const passwordValidators = [Validators.minLength(6)];
        if (this.isAddMode) {
            passwordValidators.push(Validators.required);
        }

        this.form = this.formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            username: ['', Validators.required],
            password: ['', passwordValidators],
            email: ['', Validators.required],
            phoneNumber: ['', Validators.required]
        });

        if (!this.isAddMode) {
            this.accountService.getById(this.id)
                .pipe(first())
                .subscribe(x => {
                 
                    this.formcontrolValues.firstName.setValue(x.firstName);
                    this.formcontrolValues.lastName.setValue(x.lastName);
                    this.formcontrolValues.username.setValue(x.username);
                    this.formcontrolValues.email.setValue(x.email);
                    this.formcontrolValues.phoneNumber.setValue(x.phoneNumber);
                });
        }
    }
    get formcontrolValues() { return this.form.controls; }

    onSubmit() {
        this.submitted = true;
        this.alertService.clear();
        if (this.form.invalid) {
            return;
        }

        this.loading = true;
        if (this.isAddMode) {
            this.createUser();
        } else {
            this.updateUser();
        }
    }

    private createUser() {
        this.accountService.register(this.form.value)
            .pipe(first())
            .subscribe(
                (data:ResponseModel) => {
                    this.alertService.success(data.successMessage, { keepAfterRouteChange: true });
                    this.router.navigate(['.', { relativeTo: this.route }]);
                },
                (error:ResponseModel) => {
                    this.alertService.error(error.errorMessage);
                    this.loading = false;
                });
    }

    private updateUser() {
        this.accountService.update(this.id, this.form.value)
            .pipe(first())
            .subscribe(
                (data:ResponseModel) => {
                    this.alertService.success(data.successMessage, { keepAfterRouteChange: true });
                    this.router.navigate(['..', { relativeTo: this.route }]);
                },
                (error:ResponseModel) => {
                    this.alertService.error(error.errorMessage);
                    this.loading = false;
                });
    }
}