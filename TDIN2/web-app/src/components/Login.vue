<template>
    <div class="auth">
        <h1 class="logo"><span>Book</span>Shop</h1>
        <div class="title-wrapper">
            <hr>
            <h1>Login</h1>
            <hr>
        </div>
        <form>
            <v-text-field
              v-model="email"
              :error-messages="emailErrors"
              label="E-mail"
              required
              @input="$v.email.$touch()"
              @blur="$v.email.$touch()"
            ></v-text-field>
            <v-text-field
              v-model="password"
              :error-messages="passwordErrors"
              label="Password"
              required
              @input="$v.password.$touch()"
              @blur="$v.password.$touch()"
              type="password"
              autocomplete
            ></v-text-field>
            <div class="login-buttons">
                <router-link to="/register">Register</router-link>
                <v-btn class="submit-login" round color="accent" @click="submit">Login</v-btn>
            </div>
            
        </form>
    </div>
</template>
    
<script>
    import { validationMixin } from 'vuelidate'
    import { required, maxLength, email } from 'vuelidate/lib/validators'

    export default {
        name: 'login',
        mixins: [validationMixin],

        validations: {
            email: { required, email },
            password: { required },
        },

        data: () => ({
        email: '',
        password: '',
        }),

        computed: {
            emailErrors () {
                const errors = []
                if (!this.$v.email.$dirty) return errors
                !this.$v.email.email && errors.push('Must be valid e-mail')
                !this.$v.email.required && errors.push('E-mail is required')
                return errors
            },
            passwordErrors () {
                const errors = []
                if (!this.$v.password.$dirty) return errors
                !this.$v.password.required && errors.push('Password is required')
                return errors
            }
        },

        methods: {
        submit () {
            this.$v.$touch()
        },
    }
  }
</script>
    
<style>

.auth h1.logo{ 
    font-size: 6.2em;
    margin-left: 39.5%;
    margin-top: 1.7em;
}

.auth div.title-wrapper {
    margin-bottom: 2em;
}

.auth div.title-wrapper hr:first-child{
    width: 70%;
}

form{
    display: flex;
    flex-direction: column;
    margin-left: 40%;
    width: 28em;
}

form div.v-input__control{
    margin-bottom: 1.2em;
}

div.login-buttons{
    margin-right: 0;
    margin-left: auto;
}

div.login-buttons a{
    text-decoration: none;
    font-size: 1.1em;
    padding: 0;
    padding-right: .7em;
}

</style>