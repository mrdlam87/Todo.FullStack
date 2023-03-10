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
    this.userService.deleteUser();
  }

  onAddClick() {
    this.modalService.modalIsVisibleChanged.next(false);
    this.data.isEdit
      ? this.userService.updateUser(this.userForm.value)
      : this.userService.addUser(this.userForm.value);
  }

  private initForm() {
    let name = '';

    if (this.data.isEdit) {
      name = this.userService.getCurrentUser().name;
    }

    this.userForm = new FormGroup({
      name: new FormControl(name, Validators.required),
    });
  }
}
