import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LayoutComponent } from './Layout.component';
import { LoginComponent } from './login/login.component';



const routes: Routes = [

{path:'',component:LayoutComponent,children:[
    { path: 'home', component: HomeComponent },
    {path:'login',component:LoginComponent}
]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LayoutRoutingModule { }
