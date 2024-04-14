export interface isCorrectStatus {
    status: boolean,
    message: string
}

export interface IsCorrectLogIn {
    logLogin: isCorrectStatus,
    logPassword: isCorrectStatus
}
export interface IsCorrectRegistration {
    regUsername: isCorrectStatus,
    regEmail: isCorrectStatus,
    regPassword: isCorrectStatus,
    regPasswordConfirmation: isCorrectStatus
}

export interface LoginUserData {
    login: string,
    password: string,
    rememberUser: boolean
}

export interface RegistrationUserData {
    username: string,
    email: string,
    name: string,
    surname: string,
    patronymic: string,
    password: string,
    passwordConfirmation: string,
    emailNewsletter: boolean
}

export interface AuthorizationState {
    showLogInPopup: boolean,
    loginPopupMode: string,
    serverUserMessage: string | null,
    loginUserData: LoginUserData,
    registrationUserData: RegistrationUserData,
    isCorrectLogIn: IsCorrectLogIn,
    isCorrectRegistration: IsCorrectRegistration,
    succesfulyAuthorized: boolean,
    userDoesNotExist: boolean
}