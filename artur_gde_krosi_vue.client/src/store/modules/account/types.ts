export interface editableUserData {
    name: string,
    surname: string,
    patronymic: string,
    sendingMail: string
}

export interface UserAccountState {
    editableUserData: editableUserData,
    isSucces: boolean,
    succesMessage: string
}