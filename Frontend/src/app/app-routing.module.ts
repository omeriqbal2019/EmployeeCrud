import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './_helpers';
const accountModule = () => import('./account/account.module').then(x => x.AccountModule);
const employsModule = () => import('./employs/employs.module').then(x => x.EmploysModule);

const routes: Routes = [
    { path: 'employeepage', loadChildren: employsModule, canActivate: [AuthGuard] },
    { path: 'account', loadChildren: accountModule },
    { path: '**', redirectTo: '/employeepage' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }