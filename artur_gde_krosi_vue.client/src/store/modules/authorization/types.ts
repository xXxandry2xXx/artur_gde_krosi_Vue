
export interface IsCorrect {
    logLogin: boolean,
    logPassword: boolean,
    regUsername: boolean,
    regEmail: boolean,
    regSurname: boolean,
    regPatronymic: boolean,
    regPassword: boolean,
    regPasswordConfirmation: boolean
}

export interface LoginUserData {
    login: string,
    password: string,
}

export interface RegistrationUserData {
    username: string,
    email: string,
    name: string,
    surname: string,
    patronymic: string,
    password: string,
    passwordConfirmation: string
}

export interface AuthorizationState {
    loginUserData: LoginUserData,
    registrationUserData: RegistrationUserData,
    isCorrect: IsCorrect
}