<template>

    <!-- Saved super user-->
    <div v-if="saved" class="list-group-item">
        <div v-show="editable">
            <div class="input-group">
                <input v-model="data.name" placeholder="Navn" class="form-control" />
                <input v-model="data.email" placeholder="E-post" class="form-control" />
                <div class="input-group-append">
                    <button class="btn btn-outline-secondary" v-on:click="editable = !editable" title="Avbryt"><span class="fas fa-times"></span> Avbryt</button>
                    <button class="btn btn-outline-success" v-on:click="update()" title="Oppdater"><span class="fas fa-check"></span> Oppdater</button>
                </div>
            </div>
        </div>
        <div v-show="!editable">
            <a v-bind:href="emailAsLink(superUser.email)">{{ superUser.name}} &lt;{{ superUser.email }}&gt;</a>
            <div class="btn-group float-right">
                <button class="btn btn-outline-secondary" v-on:click="editable = !editable" title="Rediger"><span class="fas fa-edit"></span></button>
                <button class="btn btn-outline-danger" v-on:click="$emit('remove')" title="Fjern"><span class="fas fa-trash"></span></button>
            </div>
        </div>
    </div>

    <!-- New super user-->
    <div v-else class="list-group-item">
        <div class="input-group">
            <input v-model="data.name" placeholder="Navn" class="form-control" />
            <input v-model="data.email" placeholder="E-post" class="form-control" />
            <div class="input-group-append">
                <button class="btn btn-outline-danger" v-on:click="$emit('remove')" title="Fjern"><span class="fas fa-trash"></span> Fjern</button>
                <button class="btn btn-outline-success" v-on:click="$parent.postSuperUser(data)" title="Legg til"><span class="fas fa-check"></span> Legg til</button>
            </div>
        </div>
    </div>

</template>

<style src="./SuperUser.scss"></style>
<script src="./SuperUser"></script>
