import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';


const routes: Routes = [
  {
        path: 'layout', // The path that triggers lazy loading
        loadChildren: () => import('./Layout/layout.module').then(m => m.LayoutModule)
      },
    { path: '', redirectTo: 'layout/home', pathMatch: 'full' },
    {
    path: 'dashboard', // ✅ lazy-load DashboardModule
    loadChildren: () =>
      import('./Dashboard/dashboard.module').then(m => m.DashboardModule)
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
