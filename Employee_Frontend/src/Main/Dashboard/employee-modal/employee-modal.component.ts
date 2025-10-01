import { Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators,ReactiveFormsModule } from '@angular/forms';
import { Modal } from 'bootstrap';
import { EmployeeService } from '../services/employee.service';

@Component({
  selector: 'app-employee-modal',
  templateUrl: './employee-modal.component.html',
  styleUrls: ['./employee-modal.component.css']
})
export class EmployeeModalComponent implements OnInit,OnChanges {


  @Input() employee: any;   // for editing
  @Output() saveEmployee = new EventEmitter<any>();

   @ViewChild('employeeModal') employeeModal!: ElementRef;

   private modalInstance!: Modal;


  employeeForm!: FormGroup;
  isEdit = false;


  constructor(private fb: FormBuilder,private employeeService: EmployeeService) {
    
  }

  ngOnInit() {

    this.initForm();
    this.patchEmployee();
    
  }

  ngOnChanges(changes: SimpleChanges): void {
    
     if (this.employee) {
        this.patchEmployee();
     }
  }




  departments = ['IT', 'HR', 'Sales', 'Admin'];
 

  private initForm(){
    this.employeeForm = this.fb.group({
  fullName: ['', [Validators.required, Validators.minLength(3)]], // required + min length 3
  email: ['', [Validators.required, Validators.email]],           // required + must be email format
  department: ['', Validators.required],                          // required
  position: [''],                                                  // optional
  hireDate: ['', Validators.required],                             // required
  dob: ['', Validators.required],                                  // required
  employeeType: ['', Validators.required],                        // required
  gender: ['', Validators.required],                               // required
  salary: [0, [Validators.required, Validators.min(0)]]            // required + minimum 0
});


  }

  patchEmployee(employee?: any){

    const formatDate = (iso: string) => iso ? iso.split('T')[0] : '';

  if (this.employee) {
      this.isEdit = true;
      const employeeDetails = {
      fullName: this.employee.fullName ?? '',
      email: this.employee.email ?? '',
      department: this.employee.department ?? '',
      position: this.employee.position ?? '',
      hireDate: formatDate(this.employee.hireDate),
      dob: formatDate(this.employee.dob),
      employeeType: this.employee.employeeType ?? '',
      gender: this.employee.gender ?? '',
      salary: this.employee.salary ?? 0
    };
    this.employeeForm.patchValue( employeeDetails);

    }

  }

 

  save() {
    if (this.employeeForm.valid) {
      
      const apiCall = this.isEdit
      ? this.employeeService.updateEmployee(this.employee.employeeId, this.employeeForm.value)
      : this.employeeService.addEmployee(this.employeeForm.value);

     apiCall.subscribe({
      next: (res) => {
        this.saveEmployee.emit(res);
        this.close();
      },
      error: (err) => {
        
      }
    });
  }
      this.employeeForm.reset();
      this.close();
    }

  open(employee?: any) {
   if (!this.modalInstance) {
    this.modalInstance = new Modal(this.employeeModal.nativeElement);
  }

  if (!employee) {
    // Create mode – reset form completely
    this.isEdit = false;
    this.employeeForm.reset();
  } else {
    // Edit mode – patch employee
    this.isEdit = true;
    this.patchEmployee(employee);
  }

  this.modalInstance.show();
  }

  close(){
  this.modalInstance.hide();
  }

 

}
