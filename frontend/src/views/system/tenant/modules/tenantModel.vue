<template>
  <a-modal
    :title="title"
    :width="800"
    :visible="visible"
    :confirmLoading="confirmLoading"
    @ok="handleOk"
    @cancel="handleCancel">
    <a-spin :spinning="spinningLoading">
      <a-form-model ref="form" :model="form" :label-col="labelCol" :wrapper-col="wrapperCol" :rules="rules">
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <!-- <a-form-model-item label="租户管理员" prop="tenantAdmin" hasFeedback>
              <a-input v-model="form.tenantAdmin" placeholder="" />
            </a-form-model-item> -->
            <a-form-model-item label="租户管理员" prop="tenantAdmin" hasFeedback>
              <a-select show-search :filter-option="filterOption" v-model="form.tenantAdmin" :disabled="false" placeholder="请选择租户管理员">
                <a-select-option v-for="(item, index) in userData" :key="index" :value="item.id">{{
                  item.name
                }}</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="租户名字" prop="name" hasFeedback>
              <a-input v-model="form.name" placeholder="" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="是否启用" prop="enable">
              <a-switch checkedChildren="启用" v-model="form.enable" unCheckedChildren="禁用" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="gutter">
          <a-col :md="mdSize" :sm="smSize">
            <a-form-model-item label="描述" prop="describe" hasFeedback>
              <a-input v-model="form.describe" placeholder="" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-divider orientation="left">邮件配置</a-divider>
        <a-row :gutter="6">
          <a-col :md="12" :sm="smSize">
            <a-form-model-item label="发送邮箱" prop="emailConfigSendEmail" hasFeedback>
              <a-input v-model="form.emailConfigSendEmail" placeholder="" />
            </a-form-model-item>
          </a-col>
          <a-col :md="12" :sm="smSize">
            <a-form-model-item label="邮箱密码" prop="emailConfigEmailPwd" hasFeedback>
              <a-input v-model="form.emailConfigEmailPwd" placeholder="" />
            </a-form-model-item>
          </a-col>
        </a-row>
        <a-row :gutter="6">
          <a-col :md="12" :sm="smSize">
            <a-form-model-item label="端口号" prop="emailConfigPort" hasFeedback>
              <a-input-number v-model="form.emailConfigPort" placeholder="" />
            </a-form-model-item>
          </a-col>
          <a-col :md="12" :sm="smSize">
            <a-form-model-item label="服务器" prop="emailConfigServer" hasFeedback>
              <a-input v-model="form.emailConfigServer" placeholder="" />
            </a-form-model-item>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
export default {
  data () {
    return {
      labelCol: {
        md: { span: 6 },
        sm: { span: 4 }
      },
      wrapperCol: {
        md: { span: 14 },
        sm: { span: 20 }
      },
      gutter: 48,
      mdSize: 24,
      smSize: 24,
      userData: [],
      formType: 'add',
      title: '添加数据',
      spinningLoading: false,
      visible: false,
      confirmLoading: false,
      rules: {
          tenantAdmin: [{ required: true, message: '请输入租户管理员！' }],
          name: [{ required: true, message: '请输入租户名字！' }],
          emailConfig: [{ required: true, message: '请输入邮件配置！' }],
          emailConfigSendEmail: [{ required: true, message: '请输入发送邮箱！' }],
          emailConfigEmailPwd: [{ required: true, message: '请输入邮箱密码！' }],
          emailConfigServer: [{ required: true, message: '请输入邮箱服务器！' }],
          emailConfigPort: [{ required: true, message: '请输入端口号！' }]
      },
      form: {}
    }
  },
  methods: {
    // 打开页面初始化
    detail (value) {
      this.visible = true
      this.confirmLoading = false
      if (this.$refs.form) {
        this.$refs.form.clearValidate()
      }
      if (value) {
        this.form = {
          ...createForm(),
          ...JSON.parse(JSON.stringify(value))
        }
        // 数据初始化
        this.form.emailConfigSendEmail = this.form.emailConfig.split(';')[0]
        this.form.emailConfigEmailPwd = this.form.emailConfig.split(';')[1]
        this.form.emailConfigServer = this.form.emailConfig.split(';')[2]
        this.form.emailConfigPort = this.form.emailConfig.split(';')[3]
        this.formType = 'edit'
        this.title = '编辑租户'
      } else {
        this.title = '新建租户'
        this.formType = 'add'
        this.form = createForm()
      }
      this.getUserData()
    },
    filterOption (input, option) {
      // value name 一起过滤
      const lowerInput = input.toLowerCase()
      const optionText = option.componentOptions.children[0].text.toLowerCase()
      const optionValue = option.componentOptions.propsData.value.toString().toLowerCase() // Convert id to string and lowercase
      return optionText.includes(lowerInput) || optionValue.includes(lowerInput)
    },
    async handleOk () {
      this.$refs.form.validate(async valid => {
        if (valid) {
          this.form.emailConfig = this.form.emailConfigSendEmail + ';' + this.form.emailConfigEmailPwd + ';' + this.form.emailConfigServer + ';' + this.form.emailConfigPort
          this.confirmLoading = true
          // 修改数据
          if (this.formType === 'edit') {
            await this.$http.put('/sysTenant', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          } else {
            // 保存数据
            // 先上传文件
            await this.$http.post('/sysTenant', this.form).then(res => {
              if (res.code === 200) {
                this.$message.success('保存成功')
                this.$emit('ok')
                this.visible = false
              }
            }).finally(() => {
              this.confirmLoading = false
            })
          }
        }
      })
    },
    async getUserData () {
      const res = await this.$http.get('/sysUser')
      if (res.code === 200) {
        this.userData = res.result
      }
    },
    handleCancel () {
      this.close()
    },
    close () {
      this.$emit('close')
      this.visible = false
    }
  }
}
function createForm () {
  return {
    enable: true,
    emailConfigSendEmail: '',
    emailConfigEmailPwd: '',
    emailConfigServer: '',
    emailConfigPort: ''
  }
}
</script>
