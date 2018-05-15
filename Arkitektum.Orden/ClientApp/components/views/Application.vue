<template>
	<div v-if="apiData">
		<div class="row">
			<div class="col-md-3 border-right">
				<sidebar-navigation 
				v-bind:navigation-items="navigationItems"></sidebar-navigation>
			</div>
			<div class="col-md-8">
				<h1>{{ apiData.name }}  - versjon {{ apiData.version }}</h1>
				
				<div class="btn-group" v-if="writeAccess">
					<a v-bind:href="apiUrls.edit" class="btn btn-sm btn-outline-primary"><i class="fas fa-edit"></i> Rediger</a>
					<a v-bind:href="apiUrls.submitAppRegistry" class="btn btn-sm btn-outline-primary"><i class="fas fa-warehouse"></i> Send inn til felles applikasjonsregister</a>
				</div>
				
				<div id="details" class="section">
					<p v-if="apiData.userModified">Sist endret av {{ apiData.userModified }} - {{ apiData.dateModified | formatDateTime }}</p>
					<p v-if="apiData.userCreated">Opprettet av: {{ apiData.userCreated }} - {{ apiData.dateCreated | formatDateTime }}</p>
					<p>Produsent: {{ apiData.vendor.name }}</p>
					<p v-if="apiData.systemOwnerName">Systemansvarlig: <span class="badge badge-info"><i class="fas fa-user"></i> {{ apiData.systemOwnerName }}</span></p>
				</div>

				<div id="cost" class="section" v-if="hasCostDetails()">
					<h2 >Kostnader</h2>
					<p v-if="apiData.annualFee != 0">Årlige kostnad: {{ apiData.annualFee }}</p>
					<p v-if="apiData.initialCost != 0">Innkjøpskostnad: {{ apiData.initialCost }}</p>
					<p v-if="apiData.purchaseDate">Anskaffelsesdato: {{ apiData.purchaseDate | formatDate }}</p>
				</div>
				
				<div id="hosting" class="section">
					<h2>Drift</h2>
					<p>Driftsleverandør: {{ apiData.hostingVendor }}</p>
					<p>Driftsplassering: {{ apiData.hostingLocationText }}</p>
					<p>Antall brukere: {{ apiData.numberOfUsers }}</p>
				</div>
				
				<div id="agreement" class="section" v-if="hasAgreementDetails()">
					<h2 >Avtale</h2>
					<p v-if="apiData.agreementDateStart">Dato inngått: {{ apiData.agreementDateStart | formatDate }}</p>
					<p v-if="apiData.agreementDescription">Beskrivelse: {{ apiData.agreementDescription }}</p>
					<p v-if="apiData.agreementTerminationClauses">Oppsigelsesbetingelser: {{ apiData.agreementTerminationClauses }}</p>
					<p v-if="apiData.agreementResponsibleRole">Avtaleansvarlig (rolle): {{ apiData.agreementResponsibleRole }}</p>
					<p v-if="apiData.agreementDocumentUrl"><a v-bind:href="apiData.agreementDocumentUrl">Lenke til avtale</a></p>
				</div>

				<div id="super-users" class="section">
					<h2>Superbrukere</h2>
					<application-super-users v-bind:organization-id="organizationId" v-bind:application-id="applicationId" route="/api/applications" v-bind:write-access="writeAccess"></application-super-users>
				</div>

				<div id="resource-links" class="section">
					<h2>Lenker</h2>
					<resource-links v-bind:parent-id="applicationId" api-path="/ResourceLinks/Application" v-bind:write-access="writeAccess"></resource-links>
				</div>

				<div class="section">
					<h2 id="sectors">Tjenesteområder</h2>
					<application-sectors v-bind:application-sectors="apiData.sectors" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-sectors>
				</div>

				<div id="dataset" class="section">
					<h2>Datasett</h2>
					<application-datasets v-bind:application-datasets="apiData.datasets" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-datasets>
				</div>
				
				<div id="national-components" class="section">
					<h2>Nasjonale felleskomponenter</h2>    
					<application-national-components v-bind:application-national-components="apiData.nationalComponents" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-national-components>
				</div>
				
				<div id="standards" class="section">
					<h2>Standarder</h2>
					<application-standards v-bind:application-standards="apiData.standards" v-bind:applicationId="applicationId" v-bind:write-access="writeAccess"></application-standards>
				</div>

			</div>
		</div>
	</div>
</template>

<script src="./Application.js"></script>
<style src="./Application.scss"></style>
