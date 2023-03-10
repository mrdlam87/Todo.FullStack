import { Component, OnInit, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalService } from 'src/app/services/modal.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.scss'],
})
export class UserFormComponent implements OnInit {
  @Input() data: { isEdit: boolean };
  title: string;
  userForm: FormGroup;

  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.title = (this.data.isEdit ? 'Update' : 'Add') + ' User';
    this.initForm();
  }

  onCloseClick() {
    this.modalService.modalIsVisibleChanged.next(false);
  }

  onCancelClick() {
    this.modalService.modalIsVisibleChanged.next(false);
  }

  onAddClick() {
    this.modalService.modalIsVisibleChanged.next(false);
    console.log(this.userForm.value);
  }

  private initForm() {
    let fullName = '';

    if (this.data.isEdit) {
      fullName = this.userService.getCurrentUser().fullName;
    }

    this.userForm = new FormGroup({
      fullName: new FormControl(fullName, Validators.required),
    });
  }
}
