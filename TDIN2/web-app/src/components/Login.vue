<template>
    <div>
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
            <v-btn round color="accent" @click="submit">Submit</v-btn>
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
                !this.$v.password.required && errors.push('E-mail is required')
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

</style>