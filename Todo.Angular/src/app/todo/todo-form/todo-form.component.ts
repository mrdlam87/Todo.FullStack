import { Component, OnInit, Input } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ModalService } from 'src/app/services/modal.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-todo-form',
  templateUrl: './todo-form.component.html',
  styleUrls: ['./todo-form.component.scss'],
})
export class TodoFormComponent implements OnInit {
  @Input() data: { isEdit: boolean };
  title: string;
  todoForm: FormGroup;

  constructor(
    private userService: UserService,
    private modalService: ModalService
  ) {}

  ngOnInit(): void {
    this.title = (this.data.isEdit ? 'Update' : 'Add') + ' Todo';
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
    console.log(this.todoForm.value);
  }

  private initForm() {
    let description = '';
    let dateCompleted = '';
    let complete = false;

    if (this.data.isEdit) {
      const currentTodo = this.userService.getCurrentUserTodo();
      description = currentTodo.description;
      dateCompleted = currentTodo.dateCompleted;
      complete = currentTodo.complete;
    }

    this.todoForm = new FormGroup({
      description: new FormControl(description, Validators.required),
      dateCompleted: new FormControl(dateCompleted, Validators.required),
      complete: new FormControl(complete),
    });
  }
}
